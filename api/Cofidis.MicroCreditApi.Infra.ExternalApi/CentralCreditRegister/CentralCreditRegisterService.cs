using Cofidis.MicroCreditApi.Infra.ExternalApi.ChaveMovelDigital;
using Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister
{
    public class CentralCreditRegisterService : ICentralCreditRegisterService
    {
        private ILogger<CentralCreditRegisterService> _logger;
        private CentralCreditRegisterSetting _setting;

        public CentralCreditRegisterService(IConfiguration configuration, ILogger<CentralCreditRegisterService> logger)
        {
            _logger = logger;
           
            var setting = configuration.GetSection("CentralCreditRegister").Get<CentralCreditRegisterSetting>();
            if (setting == null)
            {
                _logger.LogWarning("CentralCreditRegisterService Setting is empty");
                _setting = new CentralCreditRegisterSetting();
            }
            else
                _setting = setting;
        }

        public bool GetMap(string NIF, out CreditRegisterMapDto map)
        {
            var mapAux = _setting.Maps.Where(e => e.NIF == NIF).FirstOrDefault();
            if (mapAux == null)
            {
                _logger.LogError("CentralCreditRegisterService connection error"); //simulação de erro de ligação
                map = new CreditRegisterMapDto();
                return false;
            }
            
            map = mapAux;
            return true;
        }
    }
}
