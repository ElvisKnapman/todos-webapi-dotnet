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
    public async Task GetAll_ReturnsEmptyList_WhenNoTodosExist()
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
    public async Task GetAll_ReturnsListOfTodos_WhenTodosExist()
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
    public async Task GetById_ReturnsOkAndObject_WhenTodoExists()
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
    public async Task GetById_ReturnsNotFound_WhenTodoDoesntExist()
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
    public async Task Create_ReturnsNotFound_WhenUserForTodoIsNotFound()
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
    public async Task Create_ReturnsCreatedAtActionResult_WhenCreateSucceeds()
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

        _todoService.CreateAsync(Arg.Any<TodoModel>()).Returns(true);
        //_todoService.CreateAsync(Arg.Do<TodoModel>(t => todo = t)).Returns(true);

        TodoGetDto response = todo.ToGetDto();

        // Act
        var result = (CreatedAtActionResult)await _sut.Create(todoToCreate);

        // Assert
        result.StatusCode.Should().Be(201);
        result.Value.As<TodoGetDto>().Should().BeEquivalentTo(response);
        result.RouteValues!["id"].Should().Be(response.Id);
    }
}
