using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.Api.Models;

[Table("Users")]
public class UserModel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(150)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string EmailAddress { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Username { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<TodoModel> Todos = new();
}
