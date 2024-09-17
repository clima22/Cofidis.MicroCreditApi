using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Application.CreditApplication
{
    public class CreditApplicationCreateResponseDto
    {
        /// <summary>
        /// Identificador único do pedido
        /// </summary>
        public string RequestId { get; set; } = string.Empty;

        /// <summary>
        /// Resultado da avaliação
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// Descrição do resultado..
        /// </summary>
        public string ResultMessage { get; set; } = string.Empty;

        /// <summary>
        /// Valor a pagar mensalmente pelo crédito
        /// </summary>
        public decimal MonthlyPayment { get; set; }
    }
}
