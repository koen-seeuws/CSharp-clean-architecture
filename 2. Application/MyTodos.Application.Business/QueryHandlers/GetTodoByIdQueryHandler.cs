using AutoMapper;
using FluentValidation;
using MediatR;
using MyTodos.Application.Contracts.Interfaces;
using MyTodos.Application.Contracts.Interfaces.Result;
using MyTodos.Application.Contracts.Queries;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Core.Enums;

namespace MyTodos.Application.Business.QueryHandlers;

public sealed class GetTodoByIdQueryValidator : AbstractValidator<GetTodoByIdQuery>
{
    public GetTodoByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class GetTodoByIdQueryHandler(
    ITodoRepository todoRepository,
    IMapper mapper
) : IRequestHandler<GetTodoByIdQuery, Result<GetTodoByIdResponse>>
{
    public async Task<Result<GetTodoByIdResponse>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        var todo = await todoRepository.FindById(query.Id, cancellationToken);

        if (todo == null)
            return await Result<GetTodoByIdResponse>.FailedAsync($"Todo with id {query.Id} was not found",
                Reason.NotFound);

        var response = mapper.Map<GetTodoByIdResponse>(todo);

        return await Result<GetTodoByIdResponse>.SuccessAsync(response);
    }
}