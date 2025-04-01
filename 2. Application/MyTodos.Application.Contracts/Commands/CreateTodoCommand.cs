using MediatR;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Contracts.Commands;

public record CreateTodoCommand(
    string Title,
    string Assignee,
    string? Description
) : IRequest<Result<CreateTodoResponse>>;