using CommandLine;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Extensions;

namespace Yahoo_Finance_Api
{
    public class CommandLineParser
    {
        private readonly Type[] _requestTypes;
        private readonly IMediator _mediator;

        public CommandLineParser(IEnumerable<IBaseRequest> requestCollection, IMediator mediator)
        {
            _requestTypes = requestCollection.Select(i => i.GetType()).ToArray();
            _mediator = mediator;
        }

        public async Task RunArgs(string[] args)
        {
            args = GetArgs(args);

            await Parser.Default.ParseArguments(args, _requestTypes)
            .Either(async result =>
            {
                var response = await _mediator.Send(result);
                Console.WriteLine(response);
            },
            async error =>
            {
                var erorrString = string.Join(", \n", error);
                Console.WriteLine(erorrString);
            });
        }

        private static string[] GetArgs(string[] args)
        {
            if (args.Any())
            {
                return args;
            }

            Console.WriteLine("Please input a command.\n");
            args = Console.ReadLine().Split();
            return args;
        }
    }
}