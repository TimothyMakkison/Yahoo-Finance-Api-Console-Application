using CommandLine;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Yahoo_Finance_Api.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Command Line Parser Extensions.
    /// </summary>
    /// <param name="services">Service Collection to add service to.</param>
    /// <param name="assemblies">Assemblies to scan for <see cref="ICommandLineOptions"/>, <see cref="IExecuteCommandLineOptions{TCommandLineOptions,TResult}"/>, and <see cref="IExecuteParsingFailure{TResult}"/>.</param>
    public static IServiceCollection AddCommandLineParser(this IServiceCollection services,
        params Type[] assemblies)
    {
        return services.Scan(scan => scan.FromAssembliesOf(assemblies)
        .AddClasses(classes => classes.AssignableTo<IBaseRequest>().WithAttribute<VerbAttribute>())
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    }
}
