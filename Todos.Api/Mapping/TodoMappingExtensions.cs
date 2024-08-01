using Todos.Api.DTOs;
using Todos.Api.Models;

public static class TodoMappingExtensions
{
    public static TodoModel MapToTodoModel(this TodoCreateDto todo)
    {
        return new TodoModel()
        {
            Id = 0,
            Title = todo.Title?.Trim() ?? "",
            IsComplete = todo.IsComplete
        };
    }

    public static TodoModel MapToTodoModel(this TodoUpdateDto todo)
    {
        return new TodoModel()
        {
            Id = 0,
            Title = todo.Title?.Trim() ?? "",
            IsComplete = todo.IsComplete
        };
    }
}