using System.ComponentModel.DataAnnotations;

namespace Todos.Api.DTOs.User;
public class UserCreateDto
{
    [Required]
    [StringLength(150, MinimumLength = 1,
    ErrorMessage = "First name must be between 1 and 150 characters.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 1,
    ErrorMessage = "Last name must be between 1 and 150 characters.")]
    public string? LastName { get; set; }

    [Required]
    [StringLength(150, ErrorMessage = "Email must be between 1 and 200 characters.")]
    [EmailAddress(ErrorMessage = "Email not in valid format.")]
    public string? EmailAddress { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 1,
    ErrorMessage = "Username must be between 1 and 150 characters.")]
    public string? Username { get; set; }
}