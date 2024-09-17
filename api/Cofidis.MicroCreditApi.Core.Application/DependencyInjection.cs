using Cofidis.MicroCreditApi.Core.Application.CreditApplication;
using Cofidis.MicroCreditApi.Core.Application.CreditLimit;
using Cofidis.MicroCreditApi.Core.Application.EconomicIndicator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cofidis.MicroCreditApi.Core.Application
{
    public static class DependencyInjection
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddSingleton<ICreditLimitService, CreditLimitService>();
            services.AddSingleton<IEconomicIndicatorService, EconomicIndicatorService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<ICreditApplicationService, CreditApplicationService>();
        }
    }
}
