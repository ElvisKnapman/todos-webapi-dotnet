﻿using Todos.Api.Models;

namespace Todos.Api.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetAllAsync();
    Task<TodoModel?> GetByIdAsync(int id);
    Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId);
    Task<TodoModel> CreateAsync(TodoModel todo);
    Task<bool> DeleteByIdAsync(int id);
    Task<bool> UpdateAsync(TodoModel todo);
}
