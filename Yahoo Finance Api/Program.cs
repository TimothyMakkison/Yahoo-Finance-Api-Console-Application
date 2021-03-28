using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Yahoo_Finance_Api
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceProvider = Startup.BuildService();
            var parser = serviceProvider.GetRequiredService<CommandLineParser>();

            await parser.RunArgs(args);
        }
    }
}