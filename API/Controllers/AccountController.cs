using System.Text.Json;
using API.Models;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly string _emailConfirmationRedirectionUrl;
    private readonly IEmailService _emailService;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IEmailService emailService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _emailConfirmationRedirectionUrl =
            configuration["Urls:EmailConfirmationRedirection"] ?? throw new Exception("EmailConfirmationRedirection is not specified");
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto dto)
    {
        if (await _userManager.Users.FirstOrDefaultAsync(x => x.NormalizedEmail == dto.Email.Trim().ToUpper()) != null)
            return BadRequest("Email is already in use");

        var user = new User
        {
            UserName = dto.Email[..dto.Email.IndexOf('@')],
            Email = dto.Email
        };

        var results = new[]
        {
            await _userManager.CreateAsync(user, dto.Password),
            await _userManager.AddToRoleAsync(user, "User")
        };
        if (!results.All(x => x.Succeeded)) return BadRequest("Problem registering user");

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);
        await _emailService.SendConfirmationEmailAsync(user.Email, confirmationLink);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized("Either email or password is invalid");

        if (!user.EmailConfirmed) return BadRequest("Confirm your email in order to be able to sing in");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return Unauthorized("Either email or password is invalid");

        return Ok(JsonSerializer.Serialize(await _tokenService.GenerateTokenAsync(user)));
    }

    [HttpGet("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound("User not found");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded) return Redirect(_emailConfirmationRedirectionUrl);

        return BadRequest("We were unable to confirm your account");
    }
}