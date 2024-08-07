using Todos.Api.Models;
using Todos.Api.Repositories;

namespace Todos.Api.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel> CreateAsync(UserModel user) => await _userRepository.CreateAsync(user);
    public async Task<IEnumerable<UserModel>> GetAllAsync() => await _userRepository.GetAllAsync();
    public async Task<UserModel?> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);
    public async Task<bool> UpdateAsync(UserModel user) => await _userRepository.UpdateAsync(user);
    public async Task<bool> DeleteByIdAsync(int id) => await _userRepository.DeleteByIdAsync(id);
    public async Task<bool> UserExistsAsync(int userId) => await _userRepository.ExistsAsync(userId);
}