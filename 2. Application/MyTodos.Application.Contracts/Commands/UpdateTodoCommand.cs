using MediatR;
using MyTodos.Application.Contracts.Interfaces.Result;

namespace MyTodos.Application.Contracts.Commands;

public record UpdateTodoCommand(
    int Id,
    string Title,
    string Assignee,
    string? Description
) : IRequest<Result>;