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
        // Entity change tracking is not needed here
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<UserModel?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> CreateAsync(UserModel user)
    {
        _context.Users.Add(user);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> DeleteAsync(UserModel user)
    {
        _context.Users.Remove(user);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> UpdateAsync(UserModel user)
    {
        _context.Users.Update(user);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        // Entity change tracking is not needed here
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) != null;
    }
}