using Microsoft.EntityFrameworkCore;
using Todos.Api.Models;

namespace Todos.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TodoModel> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
