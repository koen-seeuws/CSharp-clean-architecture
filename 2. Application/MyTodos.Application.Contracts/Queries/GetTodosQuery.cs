using MediatR;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Contracts.Queries;

public record GetTodosQuery(
    string? Title,
    string? Assignee
) : IRequest<Result<IEnumerable<GetTodosResponseRecord>>>;