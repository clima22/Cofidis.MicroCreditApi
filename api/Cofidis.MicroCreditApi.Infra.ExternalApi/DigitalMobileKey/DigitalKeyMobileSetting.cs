using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey
{
    internal class DigitalKeyMobileSetting
    {
        public List<CustomerInfoDto> Customers { get; set; } = new List<CustomerInfoDto>();
    }
}
