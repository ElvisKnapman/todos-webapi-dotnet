using Todos.Api.Models;
using Todos.Api.Repositories;

namespace Todos.Api.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoModel>> GetAllAsync() => await _todoRepository.GetAllAsync();

    public async Task<TodoModel?> GetByIdAsync(int id) => await _todoRepository.GetByIdAsync(id);

    public async Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId) => await _todoRepository.GetAllUserTodosAsync(userId);

    public async Task<bool> CreateAsync(TodoModel todo) => await _todoRepository.CreateAsync(todo);

    public async Task<bool> UpdateAsync(TodoModel todo) => await _todoRepository.UpdateAsync(todo);

    public async Task<bool> DeleteAsync(TodoModel todo) => await _todoRepository.DeleteAsync(todo);
}
