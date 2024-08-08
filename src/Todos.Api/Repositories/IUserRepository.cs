using Todos.Api.Models;

namespace Todos.Api.Repositories;
public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllAsync();

    Task<UserModel?> GetByIdAsync(int id);

    Task<bool> CreateAsync(UserModel user);

    Task<bool> UpdateAsync(UserModel user);

    Task<bool> DeleteAsync(UserModel user);

    Task<bool> ExistsAsync(int id);
}