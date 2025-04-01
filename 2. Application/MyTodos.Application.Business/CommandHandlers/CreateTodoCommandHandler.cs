using FluentValidation;
using MediatR;
using MyTodos.Application.Contracts.Commands;
using MyTodos.Application.Contracts.Repositories;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Application.Contracts.Result;
using MyTodos.Core.Domain;

namespace MyTodos.Application.Business.CommandHandlers;

public sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Assignee).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}

public sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository
) : IRequestHandler<CreateTodoCommand, Result<CreateTodoResponse>>
{
    public async Task<Result<CreateTodoResponse>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Title = command.Title,
            Assignee = command.Assignee,
            Description = command.Description
        };

        todo = await todoRepository.Create(todo, cancellationToken);

        var response = new CreateTodoResponse(
            todo.Id,
            todo.Title,
            todo.Assignee,
            todo.Description
        );

        return await Result<CreateTodoResponse>.SuccessAsync(response);
    }
}