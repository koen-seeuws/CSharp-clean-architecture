using MediatR;

namespace MyTodos.Application.Contracts.Commands;

public record DeleteTodoCommand(int Id) : IRequest<Result.Result>;