using Todos.Api.Models;

namespace Todos.Api.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoModel>> GetAllAsync();

    Task<TodoModel?> GetByIdAsync(int id);

    Task<bool> DeleteByIdAsync(int id);

    Task<TodoModel> CreateAsync(TodoModel todo);

    Task<bool> UpdateAsync(int id, TodoModel todo);

    Task<bool> ExistsAsync(int id);
}
