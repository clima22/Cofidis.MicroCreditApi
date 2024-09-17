using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.CreditLimit
{
    internal class CreditLimitSetting
    {
        /// <summary>
        /// Idade máxima permitida no final de contrato
        /// </summary>
        public int MaxAgeFinalContract { get; set; } = 75;
        /// <summary>
        /// Duração máxima permitida do contrato de crédito
        /// </summary>
        public int MaxDurationCreditMonths { get; set; } = 72;

        /// <summary>
        /// Duração minima permitida do contrato de crédito
        /// </summary>
        public int MinDurationCreditMonths { get; set; } = 6;

        /// <summary>
        /// Montante mínimo de credito permitido
        /// </summary>
        public int MinAmountCredit { get; set; } = 500;
    }

    
}
