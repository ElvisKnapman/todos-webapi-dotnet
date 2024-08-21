using System.ComponentModel.DataAnnotations;

namespace Todos.Api.DTOs.Account;

public class RegisterDto
{
    [Required]
    public string? Username { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(10, ErrorMessage = "Password must be at least 10 characters long.")]
    public string? Password { get; set; }
}
