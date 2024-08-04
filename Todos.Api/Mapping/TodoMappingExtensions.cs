using Todos.Api.DTOs;
using Todos.Api.Models;

namespace Todo.Api.Mapping;

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

    public static TodoModel MapUpdatedTodo(this TodoModel existingTodo, TodoUpdateDto updatedTodo)
    {

        existingTodo.Id = updatedTodo.Id;
        existingTodo.Title = updatedTodo.Title?.Trim() ?? "";
        existingTodo.IsComplete = updatedTodo.IsComplete;

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