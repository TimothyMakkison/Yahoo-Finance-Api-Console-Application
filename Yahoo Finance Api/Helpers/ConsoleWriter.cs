using System;

namespace Yahoo_Finance_Api.Helpers;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(object? value)
    {
        Console.WriteLine(value.ToString());
    }
}