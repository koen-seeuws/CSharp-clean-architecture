using MediatR;
using Microsoft.Extensions.Logging;
using MyTodos.Application.Contracts.Result;
using MyTodos.Core.Enums;

namespace MyTodos.Application.Business.PipelineBehaviours;

public sealed class ExceptionHandlingPipelineBehaviour<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result, new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Exception caught while handling {RequestName} with request data: {@Request}",
                typeof(TRequest).Name, request);
            
            return new TResponse { Succeeded = false, Messages = [new Message(exception.Message, Reason.Unhandled)] };
        }
    }
}