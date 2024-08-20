using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todos.Api.DTOs.Account;
using Todos.Api.Models;

namespace Todos.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    public AccountsController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var appUser = new AppUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                if (roleResult.Succeeded)
                {
                    return Ok("User successfully created.");
                }

                return BadRequest(roleResult.Errors);
            }

            return BadRequest(createdUser.Errors);

        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}
