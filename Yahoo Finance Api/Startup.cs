using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using Yahoo_Finance_Api.Apis;
using Yahoo_Finance_Api.Extensions;
using Yahoo_Finance_Api.Handlers.Pipelines;
using Yahoo_Finance_Api.Helpers;

namespace Yahoo_Finance_Api;

public static class Startup
{
    public static IConfigurationRoot BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

        return configuration;
    }

    public static ServiceProvider CreateServiceProvider(IConfigurationRoot configuration)
    {
        var services = new ServiceCollection();

        services.AddMediatR(typeof(Program));
        services.AddAutoMapper(typeof(Program));
        services.AddCommandLineParser(typeof(Program));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddSingleton<CommandLineParser>();
        services.AddSingleton<IConsoleWriter, ConsoleWriter>();

        services.BuildRefitclient(configuration);

        return services.BuildServiceProvider();
    }

    private static void BuildRefitclient(this ServiceCollection services, IConfigurationRoot configuration)
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
