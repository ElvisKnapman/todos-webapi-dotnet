using System.ComponentModel.DataAnnotations;

namespace Todos.Api.DTOs.Todo;

public class TodoUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Todo title must be between 5 and 300 characters.")]
    public string? Title { get; set; }

    [Required]
    public bool IsComplete { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}
