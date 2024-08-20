using Microsoft.EntityFrameworkCore;
using Todos.Api.Data;
using Todos.Api.DTOs.Queries;
using Todos.Api.Models;

namespace Todos.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;

    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoModel>> GetAllAsync(GetAllTodosQuery query)
    {
        // Start building the query expression
        var todos = _context.Todos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            todos = todos.Where(todo => todo.Title.Contains(query.Title));
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        // Finalize and execute the SQL instructions
        return await todos.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync();
    }

    public async Task<TodoModel?> GetByIdAsync(int id)
    {
        return await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId)
    {
        return await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<bool> CreateAsync(TodoModel todo)
    {
        _context.Todos.Add(todo);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> DeleteAsync(TodoModel todo)
    {
        _context.Todos.Remove(todo);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> UpdateAsync(TodoModel todo)
    {
        _context.Todos.Update(todo);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Todos.AnyAsync(t => t.Id == id);
    }
}
