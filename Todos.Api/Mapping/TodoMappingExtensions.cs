using Todos.Api.DTOs.Todo;
using Todos.Api.Models;

namespace Todos.Api.Mapping;

public static class TodoMappingExtensions
{
    public static TodoModel MapToTodoModel(this TodoCreateDto todo)
    {
        return new TodoModel()
        {
            Id = 0,
            Title = todo.Title?.Trim() ?? "",
            IsComplete = todo.IsComplete,
            UserId = todo.UserId
        };
    }

    public static TodoModel MapUpdatedTodo(this TodoModel existingTodo, TodoUpdateDto updatedTodo)
    {

        existingTodo.Id = updatedTodo.Id;
        existingTodo.Title = updatedTodo.Title?.Trim() ?? "";
        existingTodo.IsComplete = updatedTodo.IsComplete;
        existingTodo.UpdatedAt = DateTime.Now;

        return existingTodo;
    }

    public static TodoGetDto MapToGetDto(this TodoModel todo)
    {
        return new TodoGetDto()
        {
            Id = todo.Id,
            Title = todo.Title,
            IsComplete = todo.IsComplete
        };
    }
}