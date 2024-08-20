using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly SignInManager<AppUser> _signInManager;
    public AccountsController(
        ITokenService tokenService,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Hey teddy, this code doesn't run and return because model validation runs right after model binding and before the action method executes");
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == loginDto.Username);

        if (user is null)
        {
            return Unauthorized("Invalid username");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password.");
        }

        var newUser = new NewUserDto()
        {
            UserName = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        };

        return Ok(newUser);
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
