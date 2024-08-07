using FluentAssertions;
using NSubstitute;
using Todos.Api.Models;
using Todos.Api.Repositories;
using Todos.Api.Services;
using Xunit.Abstractions;

namespace Todos.Api.Tests.Unit;
public class TodoServiceTests : IDisposable
{
    private readonly ITodoService _sut;

    // NSubstitute
    private readonly ITodoRepository _todoRepository = Substitute.For<ITodoRepository>();

    // Moq
    //private readonly Mock<ITodoRepository> _todoRepositoryMock = new();

    private readonly ITestOutputHelper _outputHelper;

    // Setup goes here in the constructor
    public TodoServiceTests(ITestOutputHelper outputHelper)
    {
        // NSubstitute
        _sut = new TodoService(_todoRepository);

        // Moq
        //_sut = new TodoService(_todoRepositoryMock.Object);

        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        // NSubstitute
        _todoRepository.GetAllAsync().Returns(Enumerable.Empty<TodoModel>());

        // Moq
        //_todoRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<TodoModel>());

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsersExist()
    {
        // Arrange
        var expectedUsers = new List<TodoModel>()
        {
            new TodoModel()
            {
                Id = 1,
                Title = "Todo title",
                IsComplete = false,
                UserId = 1
            },
            new TodoModel()
            {
                Id = 2,
                Title = "Another todo",
                IsComplete = true,
                UserId = 1
            }
        };

        // NSubstitute
        _todoRepository.GetAllAsync().Returns(expectedUsers);

        // Moq
        //_todoRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedUsers);

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Should().HaveCount(2);
        users.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldCallRepositoryMethodExactlyOnce_WhenInvoked()
    {
        // Arrange
        int id = 1;
        _todoRepository.GetByIdAsync(id).Returns(new TodoModel());

        // Act
        await _sut.GetByIdAsync(id);

        // Assert
        await _todoRepository.Received(1).GetByIdAsync(id);
    }

    // This is where the cleanup goes
    public void Dispose()
    {
        _outputHelper.WriteLine("Hello from cleanup");
    }
}
