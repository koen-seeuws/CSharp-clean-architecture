using MediatR;
using MyTodos.Application.Contracts.Interfaces.Result;
using MyTodos.Application.Contracts.Responses;

namespace MyTodos.Application.Contracts.Queries;

public record GetTodosQuery(
    string? Title,
    string? Assignee
) : IRequest<Result<IEnumerable<GetTodosResponseRecord>>>;