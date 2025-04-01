using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Business.PipelineBehaviours;

public sealed class LoggingPipelineBehaviour<TRequest, TResponse>(
    ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName} with request data: {@Request}", typeof(TRequest).Name, request);

        var stopwatch = Stopwatch.StartNew();

        var response = await next();

        stopwatch.Stop();

        if (response.Succeeded)
            logger.LogInformation(
                "Successfully handled {RequestName} with request data {@Request} and response: {@Response} in {ElapsedMilliseconds}ms",
                typeof(TRequest).Name, request, response, stopwatch.ElapsedMilliseconds);
        else
            logger.LogWarning(
                "Unsuccessfully handled {RequestName} with request data {@Request} and response: {@Response} in {ElapsedMilliseconds}ms",
                typeof(TRequest).Name, request, response, stopwatch.ElapsedMilliseconds);

        return response;
    }
}