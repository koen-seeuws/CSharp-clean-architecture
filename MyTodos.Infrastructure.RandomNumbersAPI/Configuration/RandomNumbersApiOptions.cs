using FluentValidation;

namespace MyTodos.Infrastructure.RandomNumbersAPI.Configuration;

public sealed class RandomNumbersApiOptions()
{
    public string BaseUrl { get; set; } = string.Empty;
}

public sealed class RandomNumbersApiOptionsValidator : AbstractValidator<RandomNumbersApiOptions>
{
    public RandomNumbersApiOptionsValidator()
    {
        RuleFor(x => x.BaseUrl).NotEmpty();
    }
}