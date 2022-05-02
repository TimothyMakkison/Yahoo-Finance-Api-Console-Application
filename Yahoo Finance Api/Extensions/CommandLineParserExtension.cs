using CommandLine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yahoo_Finance_Api.Extensions;

public static class CommandLineParserExtension
{
    public static async Task Either<T>(this ParserResult<T> result,
        Func<T, Task> parsedFunc,
        Func<IEnumerable<Error>, Task> notParsedFunc)
    {
        await result.MapResult(
            async result => await parsedFunc(result),
            async errors => await notParsedFunc(errors));
    }
}
