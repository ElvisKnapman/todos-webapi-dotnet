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

    public async Task<IEnumerable<TodoModel>> GetAsync()
    {
        return await _todoRepository.GetAsync();
    }

    public async Task<TodoModel?> GetByIdAsync(int id)
    {
        TodoModel? todo = await _todoRepository.GetByIdAsync(id);

        return todo;
    }

    public async Task<TodoModel> CreateAsync(TodoModel todo)
    {
        return await _todoRepository.CreateAsync(todo);
    }
}
