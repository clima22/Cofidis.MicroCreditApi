using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Domain.Entities
{
    public class CreditApplication
    {
        public string Id { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// Nº de contribuinte do cliente
        /// </summary>
        public string NIF { get; set; } = string.Empty;
        /// <summary>
        /// Montante de crédito solicitado em euros
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Nº de meses de duração do crédito 
        /// </summary>
        public int DurationMonths { get; set; }

        /// <summary>
        /// Redimento mensal líquido do cliente
        /// </summary>
        public decimal NetMonthlyIncome { get; set; }

        public decimal MonthlyPaymentOtherCredits { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal EffortRate { get; set; }
        public decimal IndexRisk { get; set; }
    }
}
