using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xunit_tests.DataObjects
{
    /// <summary>
    /// Contém informação para um pedido de crédito
    /// </summary>
    public class CreditApplicationCreateDto
    {
        /// <summary>
        /// Nº de contribuinte do cliente
        /// </summary>
        public string NIF { get; set; } = string.Empty;
        /// <summary>
        /// Montante de crédito pretendido em euros
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
    }
}
