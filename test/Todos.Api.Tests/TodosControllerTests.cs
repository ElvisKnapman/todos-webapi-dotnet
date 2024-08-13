using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Todos.Api.Controllers;
using Todos.Api.DTOs.Todo;
using Todos.Api.Mapping;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Tests.Unit;
public class TodosControllerTests
{
    private readonly TodosController _sut;
    private readonly ITodoService _todoService = Substitute.For<ITodoService>();
    private readonly IUserService _userService = Substitute.For<IUserService>();
    private readonly ILogger<TodosController> _logger = Substitute.For<ILogger<TodosController>>();
    public TodosControllerTests()
    {
        _sut = new TodosController(_logger, _todoService, _userService);
    }

    [Fact]
    public async Task GetAll_Returns200OkWithEmptyList_WhenNoTodosExist()
    {
        // Arrange
        IEnumerable<TodoModel> todos = Enumerable.Empty<TodoModel>();
        _todoService.GetAllAsync().Returns(todos);
        var response = todos.Select(t => t.ToGetDto());

        // Act
        var result = (OkObjectResult)await _sut.GetAll();

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(response);
        result.Value.As<IEnumerable<TodoGetDto>>().Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_Returns200OkWithListOfTodos_WhenTodosExist()
    {
        // Arrange
        List<TodoModel> todos = new()
        {
            new TodoModel
            {
                Id =1,
                Title = "Title",
                IsComplete = false,
            },
            new TodoModel
            {
                Id = 2,
                Title = "Another todo",
                IsComplete = true
            }
        };
        _todoService.GetAllAsync().Returns(todos);
        IEnumerable<TodoGetDto> response = todos.Select(t => t.ToGetDto());

        // Act
        var result = (OkObjectResult)await _sut.GetAll();

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.As<IEnumerable<TodoGetDto>>().Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task GetById_Returns200OkAndObject_WhenTodoExists()
    {
        // Arrange
        int id = 1;
        TodoModel todo = new()
        {
            Id = 1,
            Title = "Todo title",
            IsComplete = false
        };
        _todoService.GetByIdAsync(id).Returns(todo);
        TodoGetDto? response = todo.ToGetDto();

        // Act
        var result = (OkObjectResult)await _sut.GetById(id);

        // Assert
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task GetById_Returns404NotFound_WhenTodoDoesntExist()
    {
        // Arrange
        int id = 1;
        _todoService.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

        // Act
        var result = (NotFoundResult)await _sut.GetById(id);

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Create_Returns404NotFound_WhenUserForTodoIsNotFound()
    {
        // Arrange
        TodoCreateDto todoToCreate = new()
        {
            Title = "Todo title",
            IsComplete = false,
            UserId = 1
        };
        _userService.UserExistsAsync(Arg.Any<int>()).Returns(false);

        // Act
        var result = (NotFoundResult)await _sut.Create(todoToCreate);

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Create_Returns201Created_WhenCreateSucceeds()
    {
        // Arrange
        TodoCreateDto todoToCreate = new()
        {
            Title = "Todo title",
            IsComplete = false,
            UserId = 1
        };

        TodoModel todo = todoToCreate.ToTodoModel();

        _userService.UserExistsAsync(todo.UserId).Returns(true);

        _todoService.CreateAsync(Arg.Do<TodoModel>(t => todo = t)).Returns(true);
        //_todoService.CreateAsync(Arg.Do<TodoModel>(t => todo = t)).Returns(true);

        TodoGetDto response = todo.ToGetDto();

        // Act
        var result = (CreatedAtActionResult)await _sut.Create(todoToCreate);

        // Assert
        result.StatusCode.Should().Be(201);
        result.RouteValues!["id"].Should().Be(response.Id);
    }

    [Fact]
    public async Task Update_Returns400BadRequest_WhenIdsDontMatch()
    {
        // Arrange
        int id = 2;
        TodoUpdateDto updatedTodo = new()
        {
            Id = 1,
            Title = "A title",
            IsComplete = true
        };

        // Act
        var result = (BadRequestResult)await _sut.Update(id, updatedTodo);

        // Assert
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Update_Returns404NotFound_WhenTodoDoesntExist()
    {
        // Arrange
        int id = 1;
        TodoUpdateDto updatedTodo = new()
        {
            Id = 1,
            Title = "A title",
            IsComplete = true
        };
        _todoService.GetByIdAsync(id).ReturnsNull();

        // Act
        var result = (NotFoundResult)await _sut.Update(id, updatedTodo);

        // Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Update_ShouldReturn400BadRequest_WhenUpdateFails()
    {
        // Arrange
        int id = 1;
        TodoUpdateDto updatedTodo = new()
        {
            Id = 1,
            Title = "An updated title",
            IsComplete = true
        };
        TodoModel existingTodo = new()
        {
            Id = 1,
            Title = "Change me",
            IsComplete = false
        };
        _todoService.GetByIdAsync(id).Returns(existingTodo);
        existingTodo = existingTodo.ToTodoModel(updatedTodo);
        // This line below is the condition to check for result (failed update)
        _todoService.UpdateAsync(existingTodo).Returns(false);

        // Act
        var result = (BadRequestResult)await _sut.Update(id, updatedTodo);

        // Assert
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Update_ShouldReturn200Ok_WhenUpdateSucceeds()
    {
        // Arrange
        int id = 1;
        TodoUpdateDto updatedTodo = new()
        {
            Id = 1,
            Title = "An updated title",
            IsComplete = true
        };
        TodoModel existingTodo = new()
        {
            Id = 1,
            Title = "Change me",
            IsComplete = false
        };
        _todoService.GetByIdAsync(id).Returns(existingTodo);
        existingTodo = existingTodo.ToTodoModel(updatedTodo);
        _todoService.UpdateAsync(existingTodo).Returns(true);

        // Act
        var result = (OkResult)await _sut.Update(id, updatedTodo);

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Delete_ShouldReturn404NotFound_WhenTodoDoesntExist()
    {
        // Arrange
        int id = 1;
        _todoService.GetByIdAsync(id).ReturnsNull();

        // Act
        var result = (NotFoundResult)await _sut.Delete(id);

        //Assert
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Delete_ShouldReturn400BadRequest_WhenDeleteFails()
    {
        // Arrange
        int id = 1;
        TodoModel todo = new();
        _todoService.GetByIdAsync(id).Returns(todo);
        _todoService.DeleteAsync(todo).Returns(false);

        // Act
        var result = (BadRequestResult)await _sut.Delete(id);

        // Assert
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Delete_ShouldReturn200Ok_WhenDeleteSucceeds()
    {
        // Arrange
        int id = 1;
        TodoModel todo = new()
        {
            Id = 1,
            Title = "Todo title",
            IsComplete = false
        };
        _todoService.GetByIdAsync(id).Returns(todo);
        _todoService.DeleteAsync(todo).Returns(true);

        // Act 
        var result = (OkResult)await _sut.Delete(id);

        // Assert
        result.StatusCode.Should().Be(200);
    }
}
