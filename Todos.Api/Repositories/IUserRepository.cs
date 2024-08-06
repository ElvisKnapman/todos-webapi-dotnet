using Todos.Api.Models;

namespace Todos.Api.Repositories;
public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllAsync();

    Task<UserModel?> GetByIdAsync(int id);

    //Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId);

    Task<UserModel> CreateAsync(UserModel user);

    Task<bool> UpdateAsync(UserModel user);

    Task<bool> DeleteByIdAsync(int id);

    Task<bool> ExistsAsync(int id);
}