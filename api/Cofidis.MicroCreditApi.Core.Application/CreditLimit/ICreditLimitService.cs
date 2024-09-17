using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.CreditLimit
{
    public interface ICreditLimitService
    {
        /// <summary>
        /// Validar duração de crédito
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="creditDurationMonths"></param>
        /// <returns></returns>
        bool ValidateDurationCredit(DateTime birthDate, int creditDurationMonths);

        /// <summary>
        /// Validar montante mínimo e máximo de credito permitido
        /// </summary>
        /// <param name="creditAmount"></param>
        /// <param name="netMonthlyIncome"></param>
        /// <returns></returns>
        bool ValidateCreditAmountLimit(decimal creditAmount, decimal netMonthlyIncome);
    }
}
