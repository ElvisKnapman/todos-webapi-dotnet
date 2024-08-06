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
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserModel> users = await _userService.GetAllAsync();
            // Map to DTOs
            IEnumerable<UserGetDto> response = users.Select(u => u.MapToGetDto());

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            UserModel? user = await _userService.GetByIdAsync(id);

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
            // TODO: Check if the username requested has been taken

            UserModel user = userToCreate.MapToUserModel();

            user = await _userService.CreateAsync(user);

            UserGetDto response = user.MapToGetDto();

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserUpdateDto updatedUser)
        {
            // Confirm route id matches DTO id
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            // Get existing User
            UserModel? existingUser = await _userService.GetByIdAsync(id);

            // Return not found if null
            if (existingUser is null)
            {
                return NotFound();
            }

            // Map the DTO changes to the tracked entity
            existingUser = existingUser.MapUserUpdates(updatedUser);

            // Do the update
            var wasUpdateSuccessful = await _userService.UpdateAsync(existingUser);

            // Check that the update was successful
            if (!wasUpdateSuccessful)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Return the appropriate response
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool wasDeleted = await _userService.DeleteByIdAsync(id);

            return wasDeleted ? NoContent() : NotFound();
        }
    }
}