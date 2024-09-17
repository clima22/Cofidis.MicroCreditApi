using Cofidis.MicroCreditApi.Core.Domain.Repositories;
using Cofidis.MicroCreditApi.Infra.Repository.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cofidis.MicroCreditApi.Infra.Repository
{
    public static class DependencyInjection
    {
        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<ICreditApplicationRepo, CreditApplicationRepo>();
        }
    }
}
