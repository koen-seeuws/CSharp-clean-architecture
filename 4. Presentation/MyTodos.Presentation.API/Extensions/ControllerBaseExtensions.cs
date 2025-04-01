using System.Net;
using Microsoft.AspNetCore.Mvc;
using MyTodos.Application.Contracts.Interfaces.Result;
using MyTodos.Core.Enums;

namespace MyTodos.Presentation.API.Extensions;

public static class ControllerBaseExtensions
{
    public static IActionResult MatchActionResult<T>(
        this ControllerBase controllerBase,
        Result<T> result
    ) where T : notnull
        => result.Succeeded ? controllerBase.Ok(result.Data) : controllerBase.Fail(result.Messages);


    public static IActionResult MatchActionResult<T>(
        this ControllerBase controllerBase,
        Result<T> result,
        Func<ControllerBase, T, ActionResult> onSuccess
    ) where T : notnull
        => result.Succeeded ? onSuccess(controllerBase, result.Data) : controllerBase.Fail(result.Messages);
    
    
    public static IActionResult MatchActionResult(
        this ControllerBase controllerBase,
        Result result
    )
        => result.Succeeded ? controllerBase.NoContent() : controllerBase.Fail(result.Messages);

    private static IActionResult Fail(this ControllerBase controllerBase, IEnumerable<Message> messages)
    {
        var firstMessage = messages.FirstOrDefault();

        return firstMessage?.Reason switch
        {
            Reason.NotFound => controllerBase.NotFound(messages),
            Reason.Unauthorized => controllerBase.Unauthorized(messages),
            Reason.Dependency => controllerBase.StatusCode((int)HttpStatusCode.FailedDependency, messages),
            Reason.Unhandled => controllerBase.StatusCode((int)HttpStatusCode.InternalServerError, messages),
            _ => controllerBase.BadRequest(messages)
        };
    }
}