using System.Security.Claims;
using Logic;
using Microsoft.AspNetCore.Mvc;

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
        //Debugging
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }


        //Function
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();
        string? userName = _userService.GetUserNameById(int.Parse(userId));
        if (userName == null)
            return NotFound();
        return Ok(userName);
    }
}