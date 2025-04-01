using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTodos.Application.Contracts.Commands;
using MyTodos.Application.Contracts.Queries;
using MyTodos.Application.Contracts.Requests;
using MyTodos.Presentation.API.Extensions;

namespace MyTodos.Presentation.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController(
    IMediator mediator,
    IMapper mapper
) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest createTodoRequest)
    {
        var command = mapper.Map<CreateTodoCommand>(createTodoRequest);
        var result = await mediator.Send(command);
        return this.MatchActionResult(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetTodosQuery(null, null);
        var result = await mediator.Send(query);
        return this.MatchActionResult(result);
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var query = new GetTodoByIdQuery(id);
        var result = await mediator.Send(query);
        return this.MatchActionResult(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? assignee)
    {
        var query = new GetTodosQuery(title, assignee);
        var result = await mediator.Send(query);
        return this.MatchActionResult(result);
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromBody] UpdateTodoRequest updateTodoRequest)
    {
        var command = mapper.Map<UpdateTodoCommand>(updateTodoRequest);
        var result = await mediator.Send(command);
        return this.MatchActionResult(result);
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var command = new DeleteTodoCommand(id);
        var result = await mediator.Send(command);
        return this.MatchActionResult(result);
    }
}