﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLine;
using CommandLineParser.DependencyInjection.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLineParser.DependencyInjection
{
    public class CommandLineParser<TResult> : ICommandLineParser<TResult>
    {
        private static readonly Type ExecuteCommandLineOptionsInterfaceType = typeof(IExecuteCommandLineOptions<,>);
        private readonly Type[] _commandLineOptionTypes;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ICommandLineOptions> _commandLineOptions;

        public CommandLineParser(IEnumerable<ICommandLineOptions> commandLineOptions, IServiceProvider serviceProvider)
        {
            _commandLineOptions = commandLineOptions;
            _commandLineOptionTypes = commandLineOptions.Select(i => i.GetType()).ToArray();
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Parse Command Line Arguments using <see cref="Parser"/>.
        /// </summary>
        /// <param name="args">Command Line Arguments.</param>
        /// <param name="configuration">Optional Parser Configuration Action.</param>
        /// <returns>Result [code].</returns>
        public TResult ParseArguments(string[] args, Action<ParserSettings> configuration)
        {
            using (var parser = configuration == null
                ? new Parser()
                : new Parser(configuration))
            {
                var result = _commandLineOptionTypes.Count() == 1 && _commandLineOptionTypes.All(i => !i.GetCustomAttributes<VerbAttribute>().Any())
                    ? parser.ParseArguments<object>(() => _commandLineOptions.First(), args)
                    : parser.ParseArguments(args, _commandLineOptionTypes);


                if (result.Tag == ParserResultType.Parsed)
                {
                    var type =
                        ExecuteCommandLineOptionsInterfaceType.MakeGenericType(result.TypeInfo.Current,
                            typeof(TResult));
                    var methodType = type.GetMethod("Execute");
                    var executingService = _serviceProvider.GetRequiredService(type);
                    if (result is Parsed<object> parsed)
                        return (TResult)methodType.Invoke(executingService, new[] { parsed.Value });
                }
                var service = _serviceProvider.GetService<IExecuteParsingFailure<TResult>>();
                return service == null
                    ? default(TResult)
                    : service.Execute(args, (result as NotParsed<object>)?.Errors ?? Enumerable.Empty<Error>());
            }
        }
    }
}