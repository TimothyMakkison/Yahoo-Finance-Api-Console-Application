using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Extensions;
using Yahoo_Finance_Api.Handlers.Pipelines;

namespace Yahoo_Finance_Api
{
    public static class Startup
    {
        public static ServiceProvider BuildService()
        {
            var configuration = BuildConfiguration();
            var serviceProvider = BuildServiceProvider(configuration);
            return serviceProvider;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
        }

        private static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            ConfigureServices(configuration, services);

            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IConfigurationRoot configuration, ServiceCollection services)
        {
            services.AddMediatR(typeof(Program));
            services.AddAutoMapper(typeof(Program));
            services.AddCommandLineParser(typeof(Program));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            BuildRefitclient(configuration, services);
            services.AddSingleton<CommandLineParser>();
        }

        private static void BuildRefitclient(IConfigurationRoot configuration, ServiceCollection services)
        {
            services.AddRefitClient<IStockApi>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(configuration["YahooFinanceApi:BaseAddress"]);
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", configuration["YahooFinanceApi:x-rapidapi-key"]);
                    client.DefaultRequestHeaders.Add("x-rapidapi-host", configuration["YahooFinanceApi:x-rapidapi-host"]);
                });
        }
    }
}