using System.ComponentModel.DataAnnotations;

namespace Todos.Api.DTOs.Todo;

public class TodoCreateDto
{
    [Required]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Todo title must be between 5 and 300 characters.")]
    public string? Title { get; set; }
    public bool IsComplete { get; set; }

    [Required(ErrorMessage = "The user ID is required.")]
    public int? UserId { get; set; }
}
