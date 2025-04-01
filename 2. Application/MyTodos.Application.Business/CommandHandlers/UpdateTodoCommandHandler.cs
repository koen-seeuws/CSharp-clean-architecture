using FluentValidation;
using MediatR;
using MyTodos.Application.Contracts.Commands;
using MyTodos.Application.Contracts.Interfaces;
using MyTodos.Application.Contracts.Interfaces.Result;
using MyTodos.Core.Enums;

namespace MyTodos.Application.Business.CommandHandlers;

public sealed class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Assignee).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}

public sealed class UpdateTodoCommandHandler(
    ITodoRepository todoRepository
) : IRequestHandler<UpdateTodoCommand, Result>
{
    public async Task<Result> Handle(UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.FindById(command.Id, cancellationToken);

        if (todo == null)
            return await Result.FailedAsync($"Todo with id {command.Id} was not found for update", Reason.NotFound);

        todo.Title = command.Title;
        todo.Assignee = command.Assignee;
        todo.Description = command.Description;

        await todoRepository.Update(todo, cancellationToken);
        
        return await Result.SuccessAsync();
    }
}