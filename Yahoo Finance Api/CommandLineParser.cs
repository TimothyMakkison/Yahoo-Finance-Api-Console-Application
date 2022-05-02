using CommandLine;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Extensions;
using Yahoo_Finance_Api.Helpers;

namespace Yahoo_Finance_Api;

public class CommandLineParser
{
    private readonly Type[] _requestTypes;
    private readonly IMediator _mediator;
    private readonly IConsoleWriter _consoleWriter;

    public CommandLineParser(IEnumerable<IBaseRequest> requestCollection, IMediator mediator, IConsoleWriter consoleWriter)
    {
        _requestTypes = requestCollection.Select(i => i.GetType()).ToArray();
        _mediator = mediator;
        _consoleWriter=consoleWriter;
    }

    public async Task RunArgs(string[] args)
    {
        args = GetArgs(args, _consoleWriter);
        _consoleWriter.WriteLine("\n");

        await Parser.Default.ParseArguments(args, _requestTypes)
        .Either(async result =>
        {
            try
            {
                var response = await _mediator.Send(result);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(f => f.ErrorMessage);
                var errorString = string.Join("\n", errors);
                _consoleWriter.WriteLine($"Error: {errorString}");
            }
            catch (Exception e)
            {
                _consoleWriter.WriteLine(e);
            }
        },
        async error =>
        {
            var erorrString = string.Join(", \n", error);
            _consoleWriter.WriteLine(erorrString);
        });
    }

    private static string[] GetArgs(string[] args, IConsoleWriter consoleWriter)
    {
        if (args.Any())
        {
            return args;
        }

        consoleWriter.WriteLine("Please input a command.\n");
        args = Console.ReadLine().Split();
        return args;
    }
}
