using Microsoft.AspNetCore.Mvc;
using Todos.Api.DTOs.Todo;
using Todos.Api.Mapping;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly ITodoService _todoService;

    public TodosController(ILogger<TodosController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<TodoModel> todos = await _todoService.GetAllAsync();
        IEnumerable<TodoGetDto> response = todos.Select(t => t.MapToGetDto());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        TodoModel? todo = await _todoService.GetByIdAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        TodoGetDto response = todo.MapToGetDto();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto todoToCreate)
    {
        TodoModel todo = todoToCreate.MapToTodoModel();

        todo = await _todoService.CreateAsync(todo);

        TodoGetDto response = todo.MapToGetDto();

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TodoUpdateDto updatedTodo)
    {
        // Confirm route id matches the DTO id
        if (id != updatedTodo.Id)
        {
            return BadRequest();
        }

        // Get The existing entity
        TodoModel? existingTodo = await _todoService.GetByIdAsync(id);

        if (existingTodo is null)
        {
            return NotFound();
        }

        // Map the changes to the existing tracked entity
        existingTodo = existingTodo.MapUpdatedTodo(updatedTodo);

        bool wasUpdateSuccessful = await _todoService.UpdateAsync(existingTodo);

        if (!wasUpdateSuccessful)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool wasDeleted = await _todoService.DeleteByIdAsync(id);

        return wasDeleted ? NoContent() : NotFound();
    }
}
