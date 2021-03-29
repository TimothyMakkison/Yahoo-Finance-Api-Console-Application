# Yahoo Finance Api Console App
A simple Yahoo Finance api console application to learn various libraries. Contains simple functions to query stock prices and summaries.

![image](https://user-images.githubusercontent.com/49349513/112772453-cef22780-9028-11eb-92be-755d615fff7b.png)

# Example Usage
In this example the historic price for Tesla is retrieved.
![image](https://user-images.githubusercontent.com/49349513/112772710-36f53d80-902a-11eb-8483-1888c6399ee7.png)

# Behavior
* When run, the app immediately calls `Startup.BuildService()`. This behaves similarly to an Asp.Net `Startup` call by loading a configuration and then building a 'ServiceCollection' by adding services.
```
services.AddMediatR(typeof(Program));
services.AddAutoMapper(typeof(Program));
services.AddCommandLineParser(typeof(Program));
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
services.AddValidatorsFromAssembly(typeof(Program).Assembly);

BuildRefitclient(configuration, services);
services.AddSingleton<CommandLineParser>();
```
* In `Program` the service collection is used to build an instance of `CommandLineParser` and args are passed to it.
* `CommandLineParser` parses the args, either printing an error/providing help or constructing an object.
* The object is passed to the `IMediator` instance and sent.
* If the object has a registered handler then it enters the mediator pipeline.
* Inside `AbstractValidator` validators matching the object type are applied, either throwing an error if validation fails or passing it to the handler.
* The corresponding handler constructs an api request and awaits a response.
* If the response is Ok then the response is mapped to a result type and returned. 


# Libraries used:
* Microsoft.Extensions.Configuration
  - Used to load appSettings.json containing the API base address, secrets and host.
* Microsoft.Extensions.DependencyInjection
  - Scans assembly for types and creates a service provider.
* CommandLineParser
  - Handles parsing input arguments into objects and providing a basic ux - help text, error messages etc. 
* Refit
  - When configured automatically turns an interface into a REST Api client.
  - Used to create IStockApi.
* MediatR 
  - Mediator pattern library, Mediator.Extensions.Microsoft.DependencyInjection automatically gets all valid handlers.
  - Receives request objects and sends them to a valid handler. Handlers send a HTTP request and map the response to a result.
  - Supports pipeline behavior to validate the requests before sending it.
* FluentValidation
  - By creating a custom IPipelineBehavior ValidationBehavior class we can requests validate being sent through MediatR.
  - Validation is performed using FluentValidation in custom classes inheriting from AbstractValidator.
  - Validators are scanned for when building services using FluentValidation.DependencyInjection, Validators are then injected into the custom ValidationBehavior class.
* AutoMapper
  - Scans the assembly for Profiles containing mapping rules.
  - IMapper takes objects and attempts to map into different types.
