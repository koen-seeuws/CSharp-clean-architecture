using MediatR;
using MyTodos.Application.Contracts.Interfaces.Result;
using MyTodos.Application.Contracts.Responses;

namespace MyTodos.Application.Contracts.Commands;

public record CreateTodoCommand(
    string Title,
    string Assignee,
    string? Description
) : IRequest<Result<CreateTodoResponse>>;