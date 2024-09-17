using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister
{
    internal class CentralCreditRegisterSetting
    {
        public List<CreditRegisterMapDto> Maps { get; set; } = new List<CreditRegisterMapDto>();
    }
}
