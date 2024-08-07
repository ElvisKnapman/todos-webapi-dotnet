using System.ComponentModel.DataAnnotations;

namespace Todos.Api.DTOs.User;

public class UserUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 1,
    ErrorMessage = "First name must be between 1 and 150 characters.")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 1,
    ErrorMessage = "Last name must be between 1 and 150 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(150, ErrorMessage = "First name must be between 1 and 150 characters.")]
    [EmailAddress(ErrorMessage = "Email not in valid format.")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    [StringLength(150, MinimumLength = 1,
    ErrorMessage = "Username must be between 1 and 150 characters.")]
    public string UserName { get; set; } = string.Empty;
}
