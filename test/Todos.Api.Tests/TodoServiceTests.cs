using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Todos.Api.Models;
using Todos.Api.Repositories;
using Todos.Api.Services;
using Xunit.Abstractions;

namespace Todos.Api.Tests.Unit;
public class TodoServiceTests : IDisposable
{
    private readonly ITodoService _sut;
    // Moq
    //private readonly Mock<ITodoRepository> _todoRepositoryMock = new();

    // NSubstitute
    private readonly ITodoRepository _todoRepository = Substitute.For<ITodoRepository>();
    private readonly ITestOutputHelper _outputHelper;

    // Setup goes here in the constructor
    public TodoServiceTests(ITestOutputHelper outputHelper)
    {
        // Moq
        //_sut = new TodoService(_todoRepositoryMock.Object);

        // NSubstitute
        _sut = new TodoService(_todoRepository);
        _outputHelper = outputHelper;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoTodosExist()
    {
        // Moq
        //_todoRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<TodoModel>());

        // Arrange
        // NSubstitute
        _todoRepository.GetAllAsync().Returns(Enumerable.Empty<TodoModel>());


        // Act
        IEnumerable<TodoModel> todos = await _sut.GetAllAsync();

        // Assert
        todos.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfTodos_WhenSomeTodosExist()
    {
        // Arrange
        var expectedTodos = new List<TodoModel>()
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
        // Moq
        //_todoRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedTodos);

        // NSubstitute
        _todoRepository.GetAllAsync().Returns(expectedTodos);

        // Act
        IEnumerable<TodoModel> todos = await _sut.GetAllAsync();

        // Assert
        todos.Should().HaveCount(2);
        todos.Should().BeEquivalentTo(expectedTodos);
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

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNoTodoExists()
    {
        // Arrange
        _todoRepository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

        // Act
        TodoModel? result = await _sut.GetByIdAsync(1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTodo_WhenTodoExists()
    {
        // Arrange
        int id = 1;
        TodoModel existingTodo = new()
        {
            Id = 1,
            Title = "Todo title",
            IsComplete = true
        };

        // Must call this repo method and _sut method with same ID
        _todoRepository.GetByIdAsync(id).Returns(existingTodo);

        // Act
        TodoModel? result = await _sut.GetByIdAsync(id);

        // Assert
        result.Should().BeEquivalentTo(existingTodo);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenCreateSucceeds()
    {
        // Arrange
        TodoModel todo = new()
        {
            Title = "My todo title",
            IsComplete = false
        };
        _todoRepository.CreateAsync(todo).Returns(true);

        // Act
        bool result = await _sut.CreateAsync(todo);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnFalse_WhenCreateFails()
    {
        // Arrange
        TodoModel todo = new();
        _todoRepository.CreateAsync(todo).Returns(false);

        // Act
        bool result = await _sut.CreateAsync(todo);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetAllUserTodosAsync_ShouldReturnEmptyList_WhenUserHasNoTodos()
    {
        // Arrange
        int userId = 1;
        _todoRepository.GetAllUserTodosAsync(userId).Returns(Enumerable.Empty<TodoModel>());

        // Act
        IEnumerable<TodoModel> result = await _sut.GetAllUserTodosAsync(userId);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllUserTodosAsync_ShouldReturnListOfTodos_WhenUserHasTodos()
    {
        // Arrange
        int userId = 1;
        List<TodoModel> todos = new()
        {
            new TodoModel(),
            new TodoModel()
        };
        // Must call this method with the same userId to get mocked return value
        _todoRepository.GetAllUserTodosAsync(userId).Returns(todos);

        // Act
        IEnumerable<TodoModel> result = await _sut.GetAllUserTodosAsync(userId);

        // Assert
        result.Should().HaveCount(todos.Count);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenUpdateSucceeds()
    {
        // Arrange
        TodoModel todo = new()
        {
            Id = 1,
            Title = "Updated title",
            IsComplete = true
        };
        _todoRepository.UpdateAsync(todo).Returns(true);

        // Act
        bool result = await _sut.UpdateAsync(todo);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenUpdateFails()
    {
        // Arrange
        TodoModel todo = new();
        _todoRepository.UpdateAsync(todo).Returns(false);

        // Act
        bool result = await _sut.UpdateAsync(todo);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenDeleteSucceeds()
    {
        // Arrange
        TodoModel todo = new();
        _todoRepository.DeleteAsync(todo).Returns(true);

        // Act
        bool result = await _sut.DeleteAsync(todo);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenDeleteFails()
    {
        // Arrange
        TodoModel todo = new();
        _todoRepository.DeleteAsync(todo).Returns(false);

        // Act
        bool result = await _sut.DeleteAsync(todo);

        // Assert
        result.Should().BeFalse();
    }

    // This is where the cleanup goes
    public void Dispose()
    {
        _outputHelper.WriteLine("Hello from cleanup");
    }
}
