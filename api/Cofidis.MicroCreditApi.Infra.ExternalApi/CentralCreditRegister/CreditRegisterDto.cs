using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister
{
    /// <summary>
    /// Contém informação sobre responsabilidade de um crédito de um cliente
    /// </summary>
    public class CreditRegisterDto
    {
        /// <summary>
        /// Data de inicio de contrato de crédito
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Data de fim de contrato de crédito
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Total em dívida em euros
        /// </summary>
        public decimal DebtAmount { get; set; }

        /// <summary>
        /// Prestação mensal do crédito em euros
        /// </summary>
        public decimal MonthlyPayment { get; set;}
        
        /// <summary>
        /// Montante em atraso ou vencido
        /// </summary>
        public decimal OverdueAmount { get; set;}
    }
}
