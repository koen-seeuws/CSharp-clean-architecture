using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTodos.Application.Contracts.RandomNumbers;

namespace MyTodos.Infrastructure.RandomNumbersAPI.Configuration;

public static class RandomNumbersApiServiceRegistration
{
    public static void RegisterRandomNumbersApiServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionKey = "RandomNumbersApi"
    )
    {
        var options =
            configuration.BindAndValidateConfiguration<RandomNumbersApiOptions, RandomNumbersApiOptionsValidator>(
                sectionKey);

        services
            .AddHttpClient("RandomNumbersClient",client =>
            {
                client.BaseAddress = new Uri(options.BaseUrl);
            })
            .AddStandardResilienceHandler();

        services.AddScoped<IRandomNumberGenerator, RandomNumbersApiService>();
    }
}