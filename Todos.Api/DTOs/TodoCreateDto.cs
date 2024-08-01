namespace Todos.Api.DTOs;

public class TodoCreateDto
{
    public string Title { get; set; } = "";
    public bool IsComplete { get; set; }
}
