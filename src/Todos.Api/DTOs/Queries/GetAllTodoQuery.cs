namespace Todos.Api.DTOs.Queries;

public class GetAllTodosQuery
{
    public string? Title { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
