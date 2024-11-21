using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Resources.Models;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : Controller
{
    //Added this comment to test the PR, please ignore TODO: Remove this comment

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [TokenValidation]
    public IActionResult Get()
    {
        int userId = (HttpContext.Items["SimplifiedUser"] as SimpleUser).UserId;
        string? userName = _userService.GetUserNameById(userId);
        if (userName == null)
            return NotFound();
        return Ok(userName);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        string? userName = _userService.GetUserNameById(id);
        if (userName == null)
            return NotFound();
        return Ok(userName);
    }
}