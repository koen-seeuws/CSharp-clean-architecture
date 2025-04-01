using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTodos.Application.Contracts.Interfaces;

namespace MyTodos.Infrastructure.DataAccess.Configuration;

public static class DataAccessServiceRegistration
{
    public static void RegisterSqliteDataAccessServices(this IServiceCollection serviceCollection,
        IConfiguration configuration, string connectionStringKey = "Database")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        serviceCollection.AddSqlite<TodoDbContext>(connectionString);

        serviceCollection.RegisterRepositories();
    }

    public static void RegisterSqlServerDataAccessServices(this IServiceCollection serviceCollection,
        IConfiguration configuration, string connectionStringKey = "Database")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        serviceCollection.AddSqlServer<TodoDbContext>(connectionString);

        serviceCollection.RegisterRepositories();
    }

    private static void RegisterRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITodoRepository, TodoRepository>();
    }
}