using API.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Email[..dto.Email.IndexOf('@')],
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded) return Ok("User created successfully");

        return BadRequest("Problem registering user");
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return BadRequest("User does not exist");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return Unauthorized("Either email or password is invalid");

        return Ok(await _tokenService.GenerateTokenAsync(user));
    }
}