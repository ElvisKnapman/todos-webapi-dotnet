using Microsoft.EntityFrameworkCore;
using Todos.Api.Models;

namespace Todos.Api.Data;

public static class SeedDatabase
{
    public static ModelBuilder SeedTodoAndUserData(this ModelBuilder modelBuilder)
    {
        List<UserModel> users = new()
        {
            new UserModel
            {
                Id = 1,
                FirstName = "Elvis",
                LastName = "Knapman",
                EmailAddress = "elvis@test.com",
                Username = "elvis",
            },
            new UserModel
            {
                Id = 2,
                FirstName = "Nick",
                LastName = "Patterson",
                EmailAddress = "nick@test.com",
                Username = "nick"
            },
            new UserModel
            {
                Id = 3,
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "jane@example.com",
                Username = "jane",

            }
        };

        List<TodoModel> todos = new()
        {
            new TodoModel
            {
                Id = 1,
                Title = "A todo",
                IsComplete = false,
                UserId = 1
            },
            new TodoModel
            {
                Id = 2,
                Title = "Wash car",
                IsComplete = false,
                UserId = 1
            },
            new TodoModel
            {
                Id = 3,
                Title = "Go to the store",
                IsComplete = false,
                UserId = 1
            },
            new TodoModel
            {
                Id = 4,
                Title = "Take out the trash",
                IsComplete = false,
                UserId = 1
            },
            new TodoModel
            {
                Id = 5,
                Title = "Water the grass",
                IsComplete = false,
                UserId = 1
            },
            new TodoModel
            {
                Id = 6,
                Title = "Nick's first todo",
                IsComplete = false,
                UserId = 2
            },
            new TodoModel
            {
                Id = 7,
                Title = "Clean room",
                IsComplete = false,
                UserId = 2
            }
        };

        modelBuilder.Entity<UserModel>()
            .HasData(users);

        modelBuilder.Entity<TodoModel>()
            .HasData(todos);

        return modelBuilder;
    }
}
