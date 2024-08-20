using Todos.Api.DTOs.Queries;
using Todos.Api.Models;

namespace Todos.Api.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetAllAsync(GetAllTodosQuery query);
    Task<TodoModel?> GetByIdAsync(int id);
    Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId);
    Task<bool> CreateAsync(TodoModel todo);
    Task<bool> DeleteAsync(TodoModel todo);
    Task<bool> UpdateAsync(TodoModel todo);
}
