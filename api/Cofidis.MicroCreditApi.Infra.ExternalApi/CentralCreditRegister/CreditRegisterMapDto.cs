using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister
{
    /// <summary>
    /// Mapa de responsabilidade de crédito de um cliente
    /// </summary>
    public class CreditRegisterMapDto
    {
        public string NIF { get; set; } = string.Empty;
        public DateTime ReferenceDate { get; set; }
        
        public List<CreditRegisterDto> CreditRegisterList { get; set; } = new List<CreditRegisterDto>();
    }
}
