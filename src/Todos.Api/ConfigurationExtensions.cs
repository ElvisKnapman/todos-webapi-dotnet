using Microsoft.EntityFrameworkCore;
using Todos.Api.Data;
using Todos.Api.Repositories;
using Todos.Api.Services;

namespace Todos.Api;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddInjections(this IServiceCollection services)
    {
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IServiceCollection AddEFCore(this IServiceCollection services, WebApplicationBuilder builder)
    {

        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        string connectionString = "";

        if (environment == "Production")
        {
            var sqlPassword = Environment.GetEnvironmentVariable("SQL_PASSWORD");
            var server = Environment.GetEnvironmentVariable("SERVER_NAME");
            var userId = Environment.GetEnvironmentVariable("USER_ID");
            var catalogName = Environment.GetEnvironmentVariable("CATALOG_NAME");
            var port = Environment.GetEnvironmentVariable("PORT");

            connectionString = $"Server={server},{port};Initial Catalog={catalogName};Persist Security Info=False;User ID={userId};Password={sqlPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        else
        {
            // For development or other environments
            connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        }

        // Configure DbContext with the correct connection string
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }
}