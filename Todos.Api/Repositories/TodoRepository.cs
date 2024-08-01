using Todos.Api.Models;

namespace Todos.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private static int _id = 4;

    private readonly List<TodoModel> _todos = new()
    {
        new TodoModel()
        {
            Id = 1,
            Title = "Todo",
            IsComplete = true,
        },
        new TodoModel()
        {
            Id =2,
            Title = "Another Todo",
            IsComplete = false
        },
        new TodoModel()
        {
            Id =3,
            Title = "Your mom",
            IsComplete = true
        }
    };

    public async Task<IEnumerable<TodoModel>> GetAllAsync()
    {
        return await Task.FromResult(_todos.AsReadOnly());
    }

    public async Task<TodoModel?> GetByIdAsync(int id)
    {
        TodoModel? todo = _todos.Find(x => x.Id == id);

        return await Task.FromResult(todo);
    }

    public async Task<TodoModel> CreateAsync(TodoModel todo)
    {
        todo.Id = _id++;
        _todos.Add(todo);

        return await Task.FromResult(todo);
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        TodoModel? todo = _todos.FirstOrDefault(t => t.Id == id);
        int index = _todos.FindIndex(t => t.Id == id);

        if (index == -1)
        {
            return await Task.FromResult(false);
        }

        _todos.RemoveAt(index);
        return await Task.FromResult(true);
    }

    public Task<bool> UpdateAsync(TodoModel todo)
    {
        int index = _todos.FindIndex(t => t.Id == todo.Id);

        if (index == -1)
        {
            return Task.FromResult(false);
        }

        _todos[index] = todo;
        return Task.FromResult(true);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await Task.FromResult(_todos.Exists(t => t.Id == id));
    }
}
