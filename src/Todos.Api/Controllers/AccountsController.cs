using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todos.Api.DTOs.Account;
using Todos.Api.Models;
using Todos.Api.Services;

namespace Todos.Api.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    public AccountsController(ITokenService tokenService, UserManager<AppUser> userManager)
    {
        _tokenService = tokenService;
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
                    var newUser = new NewUserDto()
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    };

                    return Ok(newUser);
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
