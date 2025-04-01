using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyTodos.Application.Business.PipelineBehaviours;

namespace MyTodos.Application.Business.Configuration;

public static class TodoApplicationServiceRegistration
{
    public static void RegisterTodoApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(TodoApplicationServiceRegistration).Assembly);

        // FluentValidation
        services.AddValidatorsFromAssemblyContaining(typeof(TodoApplicationServiceRegistration));

        // MediatR
        services.AddMediatR(mediatr =>
        {
            // Handlers
            mediatr.RegisterServicesFromAssemblyContaining(typeof(TodoApplicationServiceRegistration));

            // Pipeline
            mediatr
                .AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>))
                .AddOpenBehavior(typeof(ExceptionHandlingPipelineBehaviour<,>))
                .AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });
    }
}