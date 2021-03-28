# Yahoo Finance Api Console App
A simple Yahoo Finance api console application to learn various libraries.

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
