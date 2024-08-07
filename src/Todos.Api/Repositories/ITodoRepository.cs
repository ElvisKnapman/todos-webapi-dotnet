using Todos.Api.Models;

namespace Todos.Api.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<TodoModel>> GetAllAsync();
    Task<TodoModel?> GetByIdAsync(int id);
    Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int id);
    Task<bool> DeleteAsync(TodoModel todo);
    Task<TodoModel> CreateAsync(TodoModel todo);
    Task<bool> UpdateAsync(TodoModel todo);
    Task<bool> ExistsAsync(int id);
}
