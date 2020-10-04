using FinancingPMS.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancingPMS.ServiceDependencies
{
    public static class AppConfigurationServiceCollection
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureConfig>(configuration.GetSection("AzureKeyValutConfig"));

            services.Configure<AzureAadhaarBlobConfig>(configuration.GetSection("AzureAadhaarBlobConfig"));

            return services;
        }
    }
}
