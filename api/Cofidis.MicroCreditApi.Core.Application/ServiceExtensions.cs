using Cofidis.MicroCreditApi.Core.Application.CreditApplication;
using Cofidis.MicroCreditApi.Core.Application.CreditLimit;
using Cofidis.MicroCreditApi.Core.Application.EconomicIndicator;
using Microsoft.Extensions.DependencyInjection;

namespace Cofidis.MicroCreditApi.Core.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICreditLimitService, CreditLimitService>();
            services.AddSingleton<IEconomicIndicatorService, EconomicIndicatorService>();
           
            services.AddScoped<ICreditApplicationService, CreditApplicationService>();
        }
    }
}
