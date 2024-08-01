namespace Todos.Api.DTOs
{
    public class TodoUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsComplete { get; set; }
    }
}