using FluentValidation;
using MediatR;
using MyTodos.Application.Contracts.Commands;
using MyTodos.Application.Contracts.Repositories;
using MyTodos.Application.Contracts.Result;
using MyTodos.Core.Enums;

namespace MyTodos.Application.Business.CommandHandlers;

public sealed class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public sealed class DeleteTodoCommandHandler(
    ITodoRepository todoRepository
) : IRequestHandler<DeleteTodoCommand, Result>
{
    public async Task<Result> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.FindById(command.Id, cancellationToken);

        if (todo == null)
            return await Result.FailedAsync($"Todo with id {command.Id} was not found for delete", Reason.NotFound);

        await todoRepository.Delete(todo, cancellationToken);
        
        return await Result.SuccessAsync();
    }
}