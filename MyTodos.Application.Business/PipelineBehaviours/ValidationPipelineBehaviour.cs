using FluentValidation;
using MediatR;
using MyTodos.Application.Contracts.Result;
using MyTodos.Core.Enums;

namespace MyTodos.Application.Business.PipelineBehaviours;

public sealed class ValidationPipelineBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result, new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        if (!validators.Any()) return await next(cancellationToken);
        
        var failureMessages = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => new Message(failure.ErrorMessage, Reason.NotValid, failure.PropertyName))
            .Distinct()
            .ToList();
        
        if (failureMessages.Count != 0)
            return new TResponse { Succeeded = false, Messages = failureMessages };
        
        return await next(cancellationToken);
    }
}