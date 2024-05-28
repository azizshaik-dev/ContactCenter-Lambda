
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactCenter.IntentLookup.cs
{
    internal class Startup
    {
        private readonly IConfigurationRoot Configuration;
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            ConfigureLoggingAndConfigurations(services);
            IServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }

        private void ConfigureLoggingAndConfigurations(ServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
            services.AddTransient<IntentRepo>();
        }
    }
}
