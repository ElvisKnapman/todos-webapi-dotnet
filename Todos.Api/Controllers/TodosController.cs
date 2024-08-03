using Microsoft.AspNetCore.Mvc;
using Todo.Api.Mapping;
using Todos.Api.DTOs;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Controllers;

[Route("api/[controller]")]
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
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        TodoModel? todo = await _todoService.GetByIdAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto todoToCreate)
    {
        TodoModel todo = todoToCreate.MapToTodoModel();

        todo = await _todoService.CreateAsync(todo);

        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TodoUpdateDto updatedTodo)
    {
        // Confirm id of todo being updated matches the DTO id
        if (id != updatedTodo.Id)
        {
            return BadRequest();
        }

        TodoModel todo = updatedTodo.MapToTodoModel();

        bool updated = await _todoService.UpdateAsync(todo);

        if (!updated)
        {
            return NotFound();
        }

        TodoGetDto response = todo.MapToGetDto();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _todoService.DeleteByIdAsync(id);

        return deleted ? Ok() : NotFound();
    }
}
