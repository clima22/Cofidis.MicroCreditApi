using AutoMapper;
using Cofidis.MicroCreditApi.Core.Application.CreditLimit;
using Cofidis.MicroCreditApi.Core.Application.EconomicIndicator;
using Cofidis.MicroCreditApi.Core.Domain.Repositories;
using Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister;
using Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Cofidis.MicroCreditApi.Core.Application.CreditApplication
{
    public class CreditApplicationService : ICreditApplicationService
    {
        private readonly ILogger<CreditApplicationService> _logger;
      
        private readonly IEconomicIndicatorService _economicIndicatorService;
        private readonly CreditApplicationSetting _creditApplicationSetting;
        private readonly CreditApplicationCreateValidator _validatorCreate;
        private readonly ICustomerRepo _customerRepo;
        private readonly ICreditApplicationRepo _creditApplicationRepo;
        private readonly IMapper _mapper;
        public CreditApplicationService(IConfiguration configuration,
            ILogger<CreditApplicationService> logger,
            IEconomicIndicatorService economicIndicatorService,
            CreditApplicationCreateValidator validatorCreate,
            ICustomerRepo customerRepo,
            ICreditApplicationRepo creditApplicationRepo,
            IMapper mapper
        )
        {
            _logger = logger;
           
            _economicIndicatorService = economicIndicatorService;
            _validatorCreate = validatorCreate;
            _customerRepo = customerRepo;
            _creditApplicationRepo = creditApplicationRepo;
            _mapper = mapper;

            var setting = configuration.GetSection("CreditApplication").Get<CreditApplicationSetting>();
            if (setting == null)
            {
                _logger.LogWarning("CreditApplicationService Setting is empty. So, service assumes default settings");
                _creditApplicationSetting = new CreditApplicationSetting();
            }
            else
                _creditApplicationSetting = setting;
        }

        public CreditApplicationCreateResponseDto Create(CreditApplicationCreateDto request)
        {
            var valResult = _validatorCreate.Validate(request);
            if (!valResult.IsValid) return new CreditApplicationCreateResponseDto() { Result = false, ResultMessage = _validatorCreate.BuildMsgError(valResult) };

            var customerInfo = _validatorCreate.CustomerInfo;
            var mapCRC = _validatorCreate.MapCRC;

            /*Regra 1: Se o cliente tiver algum credito com valor vencido o crédito não é aceite*/
            if (mapCRC.CreditRegisterList.Where(e => e.OverdueAmount > 0).Any())
                return new CreditApplicationCreateResponseDto() { Result = false, ResultMessage = "O cliente possui créditos com valores por regularizar" };
            /*
            Regra 2: Calcular o indice de risco e verificar se está dentro dos limites permitidos
            */
            decimal monthlyPaymentOtherCredits = mapCRC.CreditRegisterList.Sum(e => e.MonthlyPayment);
            
            decimal monthlyPayment = CalculateMonthlyPayment(request.Amount, request.DurationMonths);

            decimal effortRate = CalculateEffortRate(monthlyPaymentOtherCredits + monthlyPayment, request.NetMonthlyIncome);

            var indexRisk = CalculateRiskIndex(effortRate);

            _logger.LogDebug($"NIF: {request.NIF} | MonthlyPaymentOtherCredits: {monthlyPaymentOtherCredits} | MonthlyPayment: {monthlyPayment} | EffortRate: {effortRate} | RiskIndex: {indexRisk}");

            if (indexRisk > _creditApplicationSetting.MaxRiskIndexAllowed)
                return new CreditApplicationCreateResponseDto() { Result = false, ResultMessage = "Não estão reunidas as condições necessárias para atribuição do crédito solicitado" };

            var requestId = Guid.NewGuid().ToString();

            //Simular resgitar o cliente na BD
            var customerEnt = _mapper.Map<Domain.Entities.Customer>(customerInfo);
            if (customerEnt != null) _customerRepo.Create(customerEnt);

            //Simular resgitar o pedido de credido na BD
            var creditApplEnt = _mapper.Map<Domain.Entities.CreditApplication>(request);
            if (creditApplEnt != null)
            {
                creditApplEnt.MonthlyPaymentOtherCredits = monthlyPaymentOtherCredits;
                creditApplEnt.MonthlyPayment = monthlyPayment;
                creditApplEnt.EffortRate = effortRate;
                creditApplEnt.IndexRisk = indexRisk;
                creditApplEnt.Id = requestId;
                creditApplEnt.RequestDate = DateTime.UtcNow;

                _creditApplicationRepo.Create(creditApplEnt);
            }

            return new CreditApplicationCreateResponseDto() {RequestId = requestId, Result = true, ResultMessage = $"Crédito aceite.", MonthlyPayment= monthlyPayment };
        }

        private decimal CalculateMonthlyPayment(decimal creditAmount, int numberOfPayments)
        {
            /*Calcular a prestação mensal para o credito:
            Foi utilizado a formula básica conhecida como Sistema Francês de Amortização (PMT)
            A taxa de juro a aplicar é anual.
            Formula: P = (C * i) / (1 - i)^-n  em que:
                P: prestação mensal
                C: montante do empréstimo/credito
                i: taxa de juro mensal (quando a taxa é anual divide-se por 12).
                n: número total de prestações (meses).
            */

            var monthlyInterestRate = (double)_creditApplicationSetting.AnnualInterestRate / (100.0 * 12.0);
            var paymentAmount = (monthlyInterestRate * (double)creditAmount) / (1 - Math.Pow(1 + monthlyInterestRate, numberOfPayments * -1));
            return (decimal)paymentAmount;
        }

        private decimal CalculateEffortRate(decimal monthlyPayment, decimal netMonthlyIncome)
        {
            //Taxa de esforço = (Encargos financeiros / Rendimento Líquido Total) x 100
            return (monthlyPayment / netMonthlyIncome) * (decimal)100.0;
        }

        private decimal CalculateRiskIndex(decimal effortRate)
        { /*
            Indice de risco é calculado com base na taxa de esforço tendo em conta os creditos e os indicadores economicos.
            No mapa de responsabilidade de credito existe os valores de credito por utilizar(Valor Potencial) mas neste exercicio não será levado em conta
            O Credit Score costuma ser um bom indicador para calculo do risco, mas, não será utilizado neste exercicio
            Indice de risco é um valor entre 0 e 1 e quanto menor melhor. Os vários indicadores em percentagem é ponderado com um coficiente
           */
            var riskIndexCoefficient = _creditApplicationSetting.RiskIndexCoefficient;
            var economicIndicator = _economicIndicatorService.Get();
            var riskIndex = (effortRate / (decimal)100.0 * riskIndexCoefficient.EffortRate)
                          + (economicIndicator.InflationRate / (decimal)100.0 * riskIndexCoefficient.InflationRate)
                          + (economicIndicator.UnemploymentRate / (decimal)100.0 * riskIndexCoefficient.UnemploymentRate);
            return riskIndex;
        }
    }
}
