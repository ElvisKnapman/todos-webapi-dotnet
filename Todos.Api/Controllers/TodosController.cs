using Microsoft.AspNetCore.Mvc;
using Todos.Api.DTOs;
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
    public async Task<IActionResult> Get()
    {
        return Ok(await _todoService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto todoToCreate)
    {
        if (todoToCreate.Title.Trim() == "")
        {
            return BadRequest("Todo must have a valid title");
        }

        var todo = todoToCreate.MapToTodoModel();

        todo = await _todoService.CreateAsync(todo);

        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

}
