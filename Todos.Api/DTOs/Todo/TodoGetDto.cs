﻿namespace Todos.Api.DTOs.Todo;

public class TodoGetDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}
