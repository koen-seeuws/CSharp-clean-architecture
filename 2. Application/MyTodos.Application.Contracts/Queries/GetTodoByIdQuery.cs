using MediatR;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Contracts.Queries;

public record GetTodoByIdQuery(
    int Id
) : IRequest<Result<GetTodoByIdResponse>>;