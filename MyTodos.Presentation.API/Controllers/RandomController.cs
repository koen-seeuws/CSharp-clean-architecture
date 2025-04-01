using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTodos.Application.Contracts.Queries;
using MyTodos.Presentation.API.Extensions;

namespace MyTodos.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomController(
    IMediator mediator
) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GenerateIntegers([FromQuery] int count, [FromQuery] int min, [FromQuery] int max)
    {
        var query = new GetRandomNumbersQuery(count, min, max);
        var result = await mediator.Send(query);
        return this.MatchActionResult(result);
    }
}