using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cofidis.MicroCreditApi.Core.Application.EconomicIndicator
{
    public class EconomicIndicatorService : IEconomicIndicatorService
    {
        private readonly EconomicIndicatorDto _economicIndicatorSetting;
        private readonly ILogger<EconomicIndicatorService> _logger;

        public EconomicIndicatorService(IConfiguration configuration, ILogger<EconomicIndicatorService> logger)
        {
            _logger = logger;
            var setting = configuration.GetSection("EconomicIndicator").Get<EconomicIndicatorDto>();
            if (setting == null)
            {
                _logger.LogWarning("EconomicIndicatorService Setting is empty. So, service assumes default settings");
                _economicIndicatorSetting = new EconomicIndicatorDto();
            }
            else
                _economicIndicatorSetting = setting;
        }
        public EconomicIndicatorDto Get()
        {
            //Para simplificar os valores estão no settings. Poderia-se por exemplo obter os valores na base de dados.
            return _economicIndicatorSetting;
        }
    }
}
