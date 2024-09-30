using System.ComponentModel.DataAnnotations;
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

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDto registerRequest)
    {
        if (string.IsNullOrEmpty(registerRequest.Email) || string.IsNullOrEmpty(registerRequest.Password))
        {
            return BadRequest("Email and password must be provided.");
        }

        var authService = new AuthService(dbContext);
        var registerResponse = authService.RegisterUser(registerRequest.Email, registerRequest.Password, registerRequest.IsSeller);

        return registerResponse.Result switch
        {
            AuthService.RegisterResult.EmailInUse => Conflict("Email is already in use."),
            AuthService.RegisterResult.Failure => StatusCode(500, "Failed to register user."),
            _ => Ok(new
                {
                    token = registerResponse.Token
                })
        };
    }
}

/// <summary>
/// Data Transfer Object (DTO) for login requests.
/// </summary>
public class LoginRequestDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

public class RegisterRequestDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public bool IsSeller { get; set; }
}