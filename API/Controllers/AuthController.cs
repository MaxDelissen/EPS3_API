using Logic;
using Logic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ApplicationDbContext dbContext) : Controller
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest("Email and password must be provided.");
        }

        var authService = new AuthService(dbContext);
        string? token = authService.CheckUserGenerteToken(loginRequest.Email, loginRequest.Password);
        if (token == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(new
        {
           token
        });
    }
}

/// <summary>
/// Data Transfer Object (DTO) for login requests.
/// </summary>
public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}