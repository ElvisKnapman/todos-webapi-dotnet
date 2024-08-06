using Microsoft.EntityFrameworkCore;
using Todos.Api.Data;
using Todos.Api.Models;

namespace Todos.Api.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserModel> CreateAsync(UserModel user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        UserModel? userToDelete = await GetByIdAsync(id);

        if (userToDelete is null)
        {
            return false;
        }

        _context.Users.Remove(userToDelete);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(UserModel user)
    {
        _context.Users.Update(user);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) != null;
    }
}