using Microsoft.EntityFrameworkCore;
using Todos.Api.Data;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;

    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoModel>> GetAllAsync()
    {
        return await _context.Todos.AsNoTracking().ToListAsync();
    }

    public async Task<TodoModel?> GetByIdAsync(int id)
    {
        TodoModel? todo = await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return todo;
    }

    public async Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId)
    {
        return await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<TodoModel> CreateAsync(TodoModel todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        TodoModel? todoToDelete = await GetByIdAsync(id);

        if (todoToDelete is null)
        {
            return false;
        }

        _context.Todos.Remove(todoToDelete);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(TodoModel todo)
    {
        _context.Todos.Update(todo);
        int recordsUpdated = await _context.SaveChangesAsync();

        return recordsUpdated > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id) != null;
    }
}
