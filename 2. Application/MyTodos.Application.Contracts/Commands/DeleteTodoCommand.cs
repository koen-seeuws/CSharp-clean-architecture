using MediatR;
using MyTodos.Application.Contracts.Interfaces.Result;

namespace MyTodos.Application.Contracts.Commands;

public record DeleteTodoCommand(int Id) : IRequest<Result>;