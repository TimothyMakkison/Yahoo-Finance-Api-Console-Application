using Microsoft.Extensions.DependencyInjection;
using Yahoo_Finance_Api;

var configuration = Startup.BuildConfiguration();
var serviceProvider = Startup.CreateServiceProvider(configuration);

var parser = serviceProvider.GetRequiredService<CommandLineParser>();
await parser.RunArgs(args);