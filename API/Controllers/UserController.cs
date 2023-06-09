using API.Models;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return Ok(await _userManager.Users.Select(user => new UserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Roles = user.UserRoles.Where(x => x.User == user).Select(x => x.Role.Name).ToList()
        }).ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult> RegisterAsAdmin([FromBody] RegisterDto dto)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == dto.Email.Trim().ToUpper()) != null)
            return BadRequest("Email is already in use");

        var user = new User
        {
            UserName = dto.Email[..dto.Email.IndexOf('@')],
            Email = dto.Email,
            EmailConfirmed = true
        };

        var results = new[]
        {
            await _userManager.CreateAsync(user, dto.Password),
            await _userManager.AddToRoleAsync(user, "User")
        };
        if (!results.All(x => x.Succeeded)) return BadRequest("Problem registering user");

        return Ok(new UserDto
        {
            UserName = user.UserName,
            Email = user.Email,
            Roles = (List<string>)await _userManager.GetRolesAsync(user)
        });
    }

    [HttpDelete("{email}")]
    public async Task<ActionResult> Delete([FromRoute] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return BadRequest("User does not exist");

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded) return NoContent();

        return BadRequest("Problem deleting user");
    }

    [HttpPut("{email}")]
    public async Task<ActionResult> UpdateUser([FromRoute] string email, [FromBody] RegisterDto dto)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return BadRequest("User does not exist");

        if (await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == dto.Email.Trim().ToUpper()) != null && dto.Email != email)
            return BadRequest("Email is already in use");

        user.UserName = dto.Email[..dto.Email.IndexOf('@')];
        user.Email = dto.Email;
        user.EmailConfirmed = true;
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
            return Ok(new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Roles = (List<string>)await _userManager.GetRolesAsync(user)
            });

        return BadRequest("Problem updating user");
    }
}