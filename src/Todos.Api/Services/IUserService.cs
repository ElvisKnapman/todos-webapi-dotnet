using Todos.Api.Models;

namespace Todos.Api.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllAsync();
    Task<UserModel?> GetByIdAsync(int id);
    Task<bool> CreateAsync(UserModel user);
    Task<bool> UpdateAsync(UserModel user);
    Task<bool> DeleteAsync(UserModel user);
    Task<bool> UserExistsAsync(int userId);
}