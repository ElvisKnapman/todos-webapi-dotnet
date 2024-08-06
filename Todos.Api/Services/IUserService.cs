using Todos.Api.Models;

namespace Todos.Api.Services;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllAsync();
    Task<UserModel?> GetByIdAsync(int id);
    Task<UserModel> CreateAsync(UserModel user);
    Task<bool> UpdateAsync(UserModel user);
    Task<bool> DeleteByIdAsync(int id);
    Task<bool> UserExistsAsync(int userId);
}