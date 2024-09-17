using Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Cofidis.MicroCreditApi.Infra.ExternalApi.ChaveMovelDigital
{
    public class CustomerInfoService : ICustomerInfoService
    {
        private ILogger<CustomerInfoService> _logger;
        private DigitalKeyMobileSetting _setting;
        public CustomerInfoService(IConfiguration configuration, ILogger<CustomerInfoService> logger) 
        {
            _logger = logger;
            var setting = configuration.GetSection("DigitalMobileKey").Get<DigitalKeyMobileSetting>();
            if (setting == null)
            {
                _logger.LogWarning("DigitalMobileKey.CustomerInfoService Setting is empty");
                _setting = new DigitalKeyMobileSetting();
            }
            else
                _setting = setting;
        }

        public bool GetCustomerInfo(string NIF, out CustomerInfoDto customerInfo)
        {
            customerInfo = new CustomerInfoDto();
            var ent = _setting.Customers.Where(e => e.NIF == NIF).FirstOrDefault();
            if (ent != null)
            {
                customerInfo = ent;
                return true;
            }
            return false;
        }
    }
}
