using Todos.Api.Models;

namespace Todos.Api.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetAsync();

    Task<TodoModel?> GetByIdAsync(int id);

    Task<TodoModel> CreateAsync(TodoModel todo);
}
