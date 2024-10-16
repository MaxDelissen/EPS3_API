using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        //Function
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();
        string? userName = _userService.GetUserNameById(int.Parse(userId));
        if (userName == null)
            return NotFound();
        return Ok(userName);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        //Function
        string? userName = _userService.GetUserNameById(id);
        if (userName == null)
            return NotFound();
        return Ok(userName);
    }
}