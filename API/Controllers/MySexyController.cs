using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MySexyController : Controller
{
	[HttpGet]
	public IActionResult Index()
	{
		var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
		return Ok($"Hello sexy! Your IP is {ip}");
	}
}