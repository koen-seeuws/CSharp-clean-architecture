using AutoMapper;
using MediatR;
using MyTodos.Application.Contracts.Queries;
using MyTodos.Application.Contracts.Repositories;
using MyTodos.Application.Contracts.Responses;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Business.QueryHandlers;

public class GetTodosQueryHandler(
    ITodoRepository todoRepository,
    IMapper mapper
) : IRequestHandler<GetTodosQuery, Result<IEnumerable<GetTodosResponseRecord>>>
{
    public async Task<Result<IEnumerable<GetTodosResponseRecord>>> Handle(GetTodosQuery query,
        CancellationToken cancellationToken)
    {
        var todos = await todoRepository.Search(query.Title, query.Assignee, cancellationToken);

        var response = mapper.Map<IEnumerable<GetTodosResponseRecord>>(todos);

        return await Result<IEnumerable<GetTodosResponseRecord>>.SuccessAsync(response);
    }
}