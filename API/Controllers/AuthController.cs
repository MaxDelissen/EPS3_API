using System.ComponentModel.DataAnnotations;
using DAL;
using Resources.Interfaces;
using Resources.Interfaces.IRepository;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Logs in a user and returns a token if successful.
    /// </summary>
    /// <param name="loginRequest">Login credentials (email and password).</param>
    /// <returns>Token or error message.</returns>
    /// <remarks>
    /// Example:
    ///
    ///     POST /login
    ///     {
    ///        "email": "myEmail@provider.com",
    ///        "password": "myPassword"
    ///     }
    /// </remarks>
    /// <response code="200">Returns the token when login is successful.</response>
    /// <response code="400">If the email or password is not provided.</response>
    /// <response code="401">If the login fails due to invalid credentials.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    public IActionResult Login([FromBody] LoginRequestDto loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest("Email and password must be provided.");
        }

        var authService = new AuthService(_userRepository);
        string? token = authService.CheckUserGenerateToken(loginRequest.Email, loginRequest.Password);
        if (token == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        return Ok(new
        {
           token
        });
    }

    /// <summary>
    /// Registers a new user and returns a token if successful.
    /// </summary>
    /// <param name="registerRequest">Registration details (email, password, isSeller).</param>
    /// <returns>Token or error message.</returns>
    /// <response code="200">Returns a token when registration is successful.</response>
    /// <response code="400">If the email or password is not provided.</response>
    /// <response code="409">If the email is already in use.</response>
    /// <response code="500">If there is a server error during registration.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Register([FromBody] RegisterRequestDto registerRequest)
    {
        if (string.IsNullOrEmpty(registerRequest.Email) || string.IsNullOrEmpty(registerRequest.Password))
        {
            return BadRequest("Email and password must be provided.");
        }

        var authService = new AuthService(_userRepository);
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
/// Data transfer object for login requests.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// The email of the user.
    /// </summary>
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    [Required]
    public string Password { get; set; }
}

/// <summary>
/// Data transfer object for register requests.
/// </summary>
public class RegisterRequestDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public bool IsSeller { get; set; }
}