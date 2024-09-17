using Cofidis.MicroCreditApi.Core.Application.CreditLimit;
using Cofidis.MicroCreditApi.Core.Application.EconomicIndicator;
using Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister;
using Cofidis.MicroCreditApi.Infra.ExternalApi.ChaveMovelDigital;
using Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.CreditApplication
{
    public class CreditApplicationCreateValidator : AbstractValidator<CreditApplicationCreateDto>
    {
        private readonly ILogger<CreditApplicationCreateValidator> _logger;
        private readonly ICreditLimitService _creditLimitService;
        private readonly ICustomerInfoService _customerInfoService;
        private readonly ICentralCreditRegisterService _centralCreditRegisterService;

        private CustomerInfoDto _customerInfo;
        private bool _hasCustomerInfo = false;

        private CreditRegisterMapDto _mapCRC;

        public CustomerInfoDto CustomerInfo => _customerInfo;
        public CreditRegisterMapDto MapCRC => _mapCRC;

        public CreditApplicationCreateValidator(ILogger<CreditApplicationCreateValidator> logger,
            ICreditLimitService creditLimitService,
            ICustomerInfoService customerInfoService,
            ICentralCreditRegisterService centralCreditRegisterService)
        {
            _logger = logger;
            _creditLimitService = creditLimitService;
            _customerInfoService = customerInfoService;
            _centralCreditRegisterService = centralCreditRegisterService;

            _customerInfo = new CustomerInfoDto();
            _mapCRC = new CreditRegisterMapDto();


            RuleFor(x => x).Must(z => BeGetCustomer(z)).WithMessage("Não foi possivel obter dados sobre o NIF fornecido");

            RuleFor(x => x).Must(z => BeValidateCreditLimit(z)).WithMessage("Montante de crédito fora dos limites permitidos");

            RuleFor(x => x).Must(z => BeValidateDurationLimit(z)).WithMessage("Duração de crédito fora dos limites permitidos");

            RuleFor(x => x).Must(z => BeGetCRCMap(z)).WithMessage("Neste momento não é possivel processar o seu pedido");

        }
        private bool BeGetCustomer(CreditApplicationCreateDto request)
        {
            //obter informação do cliente atraves do CMD (chave móvel digital)
            if (!_customerInfoService.GetCustomerInfo(request.NIF, out var customerInfoDto))
                return false;
            _customerInfo = customerInfoDto;
            _hasCustomerInfo = true;
            return true;
        }

        private bool BeValidateCreditLimit(CreditApplicationCreateDto request)
        {
            if (!_creditLimitService.ValidateCreditAmountLimit(request.Amount, request.NetMonthlyIncome))
                return false;
            return true;
        }

        private bool BeValidateDurationLimit(CreditApplicationCreateDto request)
        {
            if (!_hasCustomerInfo) return true; //já tem erro customer not found
            if (!_creditLimitService.ValidateDurationCredit(_customerInfo.BirthDate, request.DurationMonths))
                return false;
            return true;
        }

        private bool BeGetCRCMap(CreditApplicationCreateDto request)
        {
            if (!_hasCustomerInfo) return true; //já tem erro customer not found

            if (!_centralCreditRegisterService.GetMap(request.NIF, out var mapCRC))
                return false;

            _mapCRC = mapCRC;
            return true;
        }

        public string BuildMsgError(ValidationResult validationResult)
        {
            if (validationResult.Errors.Count > 0) return validationResult.Errors[0].ErrorMessage;
            return string.Empty;
        }
    }
}
