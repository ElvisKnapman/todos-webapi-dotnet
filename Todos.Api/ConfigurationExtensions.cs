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

        return services;
    }

    public static IServiceCollection AddEFCore(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}