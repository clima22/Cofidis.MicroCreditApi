using Cofidis.MicroCreditApi.Infra.ExternalApi.CentralCreditRegister;
using Cofidis.MicroCreditApi.Infra.ExternalApi.ChaveMovelDigital;
using Cofidis.MicroCreditApi.Infra.ExternalApi.DigitalMobileKey;
using Microsoft.Extensions.DependencyInjection;

namespace Cofidis.MicroCreditApi.Infra.ExternalApi
{
    public static class DependencyInjection
    {
        public static void ConfigureExternalApis(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerInfoService, CustomerInfoService>();
            services.AddSingleton<ICentralCreditRegisterService, CentralCreditRegisterService>();
        }
    }
}
