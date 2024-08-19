using System.Diagnostics;
using Todos.Api.DTOs.Queries;
using Todos.Api.Logging;
using Todos.Api.Models;
using Todos.Api.Repositories;

namespace Todos.Api.Services;

public class TodoService : ITodoService
{
    private readonly ILoggerAdapter<TodoService> _logger;
    private readonly ITodoRepository _todoRepository;

    public TodoService(ILoggerAdapter<TodoService> logger, ITodoRepository todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoModel>> GetAllAsync(GetAllTodosQuery query)
    {
        _logger.LogInformation("Retrieving all todos");

        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _todoRepository.GetAllAsync(query);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while retrieving all the todos");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for retrieving all todos returned in {0}ms", stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task<TodoModel?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving todo with id {0}", id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _todoRepository.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong retrieving todo with id {0}", id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for retrieving todo with id {0} returned in {1}ms", id, stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task<IEnumerable<TodoModel>> GetAllUserTodosAsync(int userId)
    {
        _logger.LogInformation("Retrieving todos for user with id {0}", userId);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _todoRepository.GetAllUserTodosAsync(userId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong retrieving todos for user with id {0}", userId);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for retrieving todos for user with id {0} returned in {1}ms", userId, stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> CreateAsync(TodoModel todo)
    {
        _logger.LogInformation("Creating a todo");
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _todoRepository.CreateAsync(todo);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong creating todo");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for creating todo returned in {0}ms", stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> UpdateAsync(TodoModel todo)
    {
        _logger.LogInformation("Updating todo with id {0}", todo.Id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _todoRepository.UpdateAsync(todo);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong updating todo with id {0}", todo.Id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for updating todo with id {0} returned in {1}ms", todo.Id, stopwatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> DeleteAsync(TodoModel todo)
    {
        _logger.LogInformation("Deleting todo with id {0}", todo.Id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _todoRepository.DeleteAsync(todo);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong deleting todo with id {0}", todo.Id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for deleting todo with id {0} returned in {1}ms", todo.Id, stopwatch.ElapsedMilliseconds);
        }
    }
}
