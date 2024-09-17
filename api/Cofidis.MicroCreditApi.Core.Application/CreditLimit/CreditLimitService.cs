using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cofidis.MicroCreditApi.Core.Application.CreditLimit
{
    public class CreditLimitService : ICreditLimitService
    {
        private readonly CreditLimitSetting _creditLimitSetting;
        private readonly ILogger<CreditLimitService> _logger;
        public CreditLimitService(IConfiguration configuration, ILogger<CreditLimitService> logger)
        {
            _logger = logger;
            var setting = configuration.GetSection("CreditLimit").Get<CreditLimitSetting>();
            if (setting == null)
            {
                _logger.LogWarning("CreditLimit Setting is empty. So, service assumes default settings");
                _creditLimitSetting = new CreditLimitSetting();
            }
            else
                _creditLimitSetting = setting;
        }

        public bool ValidateDurationCredit(DateTime birthDate, int creditDurationMonths)
        {
            var finalContractDate = DateTime.UtcNow.AddMonths(creditDurationMonths);

            //para calcular a idade só esta a ser considerado o ano de nascimento (simplificar).
            var ageFinal = finalContractDate.Year - birthDate.Year;

            return ageFinal <= _creditLimitSetting.MaxAgeFinalContract
                && creditDurationMonths <= _creditLimitSetting.MaxDurationCreditMonths
                && creditDurationMonths >= _creditLimitSetting.MinDurationCreditMonths;
        }

        public bool ValidateCreditAmountLimit(decimal creditAmount, decimal netMonthlyIncome)
        {
            //Aqui seria chamar o store procedure..
            
            if (creditAmount < _creditLimitSetting.MinAmountCredit) 
                return false;

            //Para simular a implementação do enunciado
            if (netMonthlyIncome <= 1000) return creditAmount <= 1000;
            if (netMonthlyIncome <= 2000) return creditAmount <= 2000;
            return creditAmount <= 5000;
        }
    }
}
