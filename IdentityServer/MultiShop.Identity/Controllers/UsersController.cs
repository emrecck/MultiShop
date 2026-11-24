using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Identity.Models;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace MultiShop.Identity.Controllers;

[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/users")]
public sealed class UsersController(UserManager<User> userManager) : ControllerBase
{
    [HttpGet("getuser")]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.GetClaim(Claims.Subject);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return NotFound();

        return Ok(new
        {
            Id = user.Id.ToString(),
            Username = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            Name = user.Name,
            Surname = user.Surname
        });
    }

    [HttpGet("GetAllUserList")]
    public async Task<IActionResult> GetAllUserList(CancellationToken cancellationToken)
    {
        var users = await userManager.Users
            .Select(u => new
            {
                id = u.Id.ToString(),
                name = u.Name,
                surname = u.Surname,
                userName = u.UserName ?? string.Empty,
                email = u.Email ?? string.Empty
            })
            .ToListAsync(cancellationToken);

        return Ok(users);
    }

    [HttpGet("~/api/Statistics")]
    public async Task<ActionResult<int>> GetUserCount(CancellationToken cancellationToken)
    {
        var count = await userManager.Users.CountAsync(cancellationToken);
        return Ok(count);
    }

    [AllowAnonymous]
    [HttpPost("~/api/Registers")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var user = new User
        {
            UserName = request.Username,
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("Kullanıcı başarıyla eklendi.");
    }

    public sealed record RegisterUserRequest(
        string Username,
        string Email,
        string Name,
        string Surname,
        string Password);
}
