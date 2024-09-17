using Serilog;

namespace Cofidis.MicroCreditApi.WebApi
{
    public static class LogServicesExtensions
    {
        public static Serilog.Core.Logger ConfigureLogs(this IServiceCollection services, IConfiguration configuration, ILoggingBuilder loggingBuilder)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);
            return logger;
        }
    }
}
