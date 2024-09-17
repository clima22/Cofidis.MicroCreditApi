using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey
{
    public interface ICustomerInfoService
    {
        public bool GetCustomerInfo(string NIF, out CustomerInfoDto customerInfo);
    }
}
