using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yahoo_Finance_Api.Helpers;

namespace Yahoo_Finance_Api.Handlers.Pipelines;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IConsoleWriter _consoleWriter;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IConsoleWriter consoleWriter)
    {
        _validators = validators;
        _consoleWriter=consoleWriter;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);
        var failure = _validators.Select(v => v.Validate(context))
            .SelectMany(vr => vr.Errors)
            .Where(x => x is not null)
            .ToArray();

        if (failure.Any())
        {
            throw new ValidationException(failure);
        }
        return next();
    }
}
