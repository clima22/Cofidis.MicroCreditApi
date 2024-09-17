using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Core.Domain.Entities
{
    public class Customer
    {
        /// <summary>
        /// Nº de contribuinte do cliente
        /// </summary>
        public string NIF { get; set; } = string.Empty;
        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Nº telefone do cliente
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// Data nascimento do cliente
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
