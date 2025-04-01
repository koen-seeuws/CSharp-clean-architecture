using MediatR;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Contracts.Queries;

public record GetRandomNumbersQuery(
    int Count,
    int Min,
    int Max
) : IRequest<Result<int[]>>;