using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.Api.Models;

[Table("Todos")]
public class TodoModel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;
    public bool IsComplete { get; set; }

    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
