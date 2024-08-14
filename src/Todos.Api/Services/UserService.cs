using System.Diagnostics;
using Todos.Api.Logging;
using Todos.Api.Models;
using Todos.Api.Repositories;

namespace Todos.Api.Services;
public class UserService : IUserService
{
    private readonly ILoggerAdapter<UserService> _logger;
    private readonly IUserRepository _userRepository;

    public UserService(ILoggerAdapter<UserService> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all users");
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong retrieving all the users");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for retrieving all the users returned in {0}ms", stopwatch.ElapsedMilliseconds);
        }
    }
    public async Task<UserModel?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving user with id {0}", id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong retrieving user with id {0}", id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for retrieving user with id {0} returned in {1}ms", id, stopwatch.ElapsedMilliseconds);
        }
    }
    public async Task<bool> CreateAsync(UserModel user)
    {
        _logger.LogInformation("Creating user");
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.CreateAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong creating the user");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for creating user returned in {0}ms", stopwatch.ElapsedMilliseconds);
        }
    }
    public async Task<bool> UpdateAsync(UserModel user)
    {
        _logger.LogInformation("Updating user with id {0}", user.Id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.UpdateAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong updating the user with id {0}", user.Id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for updating user with id {0} returned in {1}ms", user.Id, stopwatch.ElapsedMilliseconds);
        }
    }
    public async Task<bool> DeleteAsync(UserModel user)
    {
        _logger.LogInformation("Deleting user with id {0}", user.Id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.DeleteAsync(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong deleting user with id {0}", user.Id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for deleting user with id {0} returned in {1}ms", user.Id, stopwatch.ElapsedMilliseconds);
        }
    }
    public async Task<bool> UserExistsAsync(int id)
    {
        _logger.LogInformation("Checking if user exists with id {0}", id);
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await _userRepository.ExistsAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong checking if user exists with id {0}", id);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Result for checking if user exists with id {0} returned in {1}ms", id, stopwatch.ElapsedMilliseconds);
        }
    }
}