using Microsoft.AspNetCore.Mvc;
using Todos.Api.DTOs.User;
using Todos.Api.Mapping;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _usersService;

        public UsersController(ILogger<UsersController> logger, IUserService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserModel> users = await _usersService.GetAllAsync();
            // Map to DTOs
            IEnumerable<UserGetDto> response = users.Select(u => u.MapToGetDto());

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            UserModel? user = await _usersService.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            UserGetDto response = user.MapToGetDto();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userToCreate)
        {
            UserModel user = userToCreate.MapToUserModel();

            user = await _usersService.CreateAsync(user);

            UserGetDto response = user.MapToGetDto();

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
    }
}