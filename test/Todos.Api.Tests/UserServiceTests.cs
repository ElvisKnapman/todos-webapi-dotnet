using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Todos.Api.Models;
using Todos.Api.Repositories;
using Todos.Api.Services;
using Xunit.Abstractions;

namespace Todos.Api.Tests.Unit;
public class UserServiceTests
{
    private readonly IUserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ITestOutputHelper _outputHelper;
    public UserServiceTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _sut = new UserService(_userRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(Enumerable.Empty<UserModel>());

        // Act
        IEnumerable<UserModel> result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenSomeUsersExist()
    {
        // Arrange
        IEnumerable<UserModel> users = new List<UserModel>()
        {
            new UserModel()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                EmailAddress = "john@test.com",
                Username = "johnsmith"
            },
            new UserModel()
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Jones",
                EmailAddress = "bob@test.com",
                Username = "bobjones"
            }
        };
        _userRepository.GetAllAsync().Returns(users);

        // Act
        IEnumerable<UserModel> result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        int id = 1;
        UserModel user = new()
        {
            Id = 1,
            FirstName = "Elvis",
            LastName = "Knapman",
            EmailAddress = "elvis@test.com",
            Username = "elvis"
        };
        _userRepository.GetByIdAsync(id).Returns(user);

        // Act
        UserModel? result = await _sut.GetByIdAsync(id);

        // Assert
        result.Should().BeEquivalentTo(user);
        //await _userRepository.Received(1).GetByIdAsync(id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenUserDoesntExist()
    {
        // Arrange
        int id = 1;
        _userRepository.GetByIdAsync(id).ReturnsNull();

        // Act
        UserModel? result = await _sut.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenCreateSucceeds()
    {
        // Arrange
        UserModel user = new();
        _userRepository.CreateAsync(user).Returns(true);

        // Assert
        bool result = await _sut.CreateAsync(user);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_WhenCreateFails()
    {
        // Arrange
        UserModel user = new();
        _userRepository.CreateAsync(user).Returns(false);

        // Act
        bool result = await _sut.CreateAsync(user);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenUpdateSucceeds()
    {
        // Arrange
        UserModel user = new();
        _userRepository.UpdateAsync(user).Returns(true);

        // Act
        bool result = await _sut.UpdateAsync(user);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenUpdateFails()
    {
        // Arrange
        UserModel user = new();
        _userRepository.UpdateAsync(user).Returns(false);

        // Act
        bool result = await _sut.UpdateAsync(user);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenDeleteSucceeds()
    {
        // Arrange
        UserModel user = new();
        _userRepository.DeleteAsync(user).Returns(true);

        // Act
        bool result = await _sut.DeleteAsync(user);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenDeleteFails()
    {
        // Arrange
        UserModel user = new();
        _userRepository.DeleteAsync(user).Returns(false);

        // Act
        bool result = await _sut.DeleteAsync(user);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task UserExistsAsync_ShouldReturnTrue_WhenUserExists()
    {
        // Arrange
        int id = 1;
        _userRepository.ExistsAsync(id).Returns(true);

        // Act
        bool result = await _sut.UserExistsAsync(id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UserExistsAsync_ShouldReturnFalse_WhenUserDoesntExist()
    {
        // Arrange
        int id = 1;
        _userRepository.ExistsAsync(id).Returns(false);

        // Act
        bool result = await _sut.UserExistsAsync(id);

        // Assert
        result.Should().BeFalse();
    }
}
