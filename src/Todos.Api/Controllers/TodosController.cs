using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todos.Api.DTOs.Queries;
using Todos.Api.DTOs.Todo;
using Todos.Api.Logging;
using Todos.Api.Mapping;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Controllers;

[Route("api/todos")]
[ApiController]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ILoggerAdapter<TodosController> _logger;
    private readonly ITodoService _todoService;
    private readonly IUserService _userService;

    public TodosController(ILoggerAdapter<TodosController> logger, ITodoService todoService, IUserService userService)
    {
        _logger = logger;
        _todoService = todoService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTodosQuery query)
    {
        IEnumerable<TodoModel> todos = await _todoService.GetAllAsync(query);
        IEnumerable<TodoGetDto> response = todos.Select(t => t.ToGetDto());

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        TodoModel? todo = await _todoService.GetByIdAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        TodoGetDto response = todo.ToGetDto();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto todoToCreate)
    {
        TodoModel todo = todoToCreate.ToTodoModel();
        
        bool userExists = await _userService.UserExistsAsync(todo.UserId);

        if (!userExists)
        {
            return NotFound();
        }

        bool wasCreated = await _todoService.CreateAsync(todo);

        if (!wasCreated)
        {
            return BadRequest();
        }

        TodoGetDto response = todo.ToGetDto();

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TodoUpdateDto updatedTodo)
    {
        // Confirm route id matches the DTO id
        if (id != updatedTodo.Id)
        {
            return BadRequest();
        }
        
        TodoModel? existingTodo = await _todoService.GetByIdAsync(id);

        if (existingTodo is null)
        {
            return NotFound();
        }

        // Map the changes to the existing tracked entity
        existingTodo = existingTodo.ToTodoModel(updatedTodo);

        bool wasUpdateSuccessful = await _todoService.UpdateAsync(existingTodo);

        if (!wasUpdateSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        TodoModel? todo = await _todoService.GetByIdAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        bool wasDeleted = await _todoService.DeleteAsync(todo);

        return wasDeleted ? Ok() : BadRequest();
    }
}
