using FluentValidation;
using MediatR;
using MyTodos.Application.Contracts.Queries;
using MyTodos.Application.Contracts.RandomNumbers;
using MyTodos.Application.Contracts.Result;

namespace MyTodos.Application.Business.QueryHandlers;

public class GetRandomNumbersQueryValidator : AbstractValidator<GetRandomNumbersQuery>
{
    public GetRandomNumbersQueryValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);
        RuleFor(x => x.Min)
            .GreaterThanOrEqualTo(0)
            .LessThan(x => x.Max);
        RuleFor(x => x.Max).LessThanOrEqualTo(10000);
    }
}

public class GetRandomNumbersQueryHandler(
    IRandomNumberGenerator randomNumberGenerator
) : IRequestHandler<GetRandomNumbersQuery, Result<int[]>>
{
    public async Task<Result<int[]>> Handle(GetRandomNumbersQuery request, CancellationToken cancellationToken)
    {
        var randomNumbers = await randomNumberGenerator.GenerateRandomNumbers(request.Count, request.Min, request.Max);
        return await Result<int[]>.SuccessAsync(randomNumbers);
    }
}