using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todos.Api.Models;

namespace Todos.Api.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TodoModel> Todos { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        List<IdentityRole> roles = new()
        {
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole()
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
