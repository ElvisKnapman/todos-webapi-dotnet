using Todos.Api.Models;
using Todos.Api.Repositories;

namespace Todos.Api.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserModel> CreateAsync(UserModel user)
    {
        return await _repository.CreateAsync(user);
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(UserModel user)
    {
        return await _repository.UpdateAsync(user);
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _repository.DeleteByIdAsync(id);
    }
}