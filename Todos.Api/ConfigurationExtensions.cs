using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todos.Api.Repositories;
using Todos.Api.Services;

namespace Todos.Api
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddSingleton<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }
    }
}