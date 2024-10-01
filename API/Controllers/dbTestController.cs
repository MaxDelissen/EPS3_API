using DAL;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class dbTestController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        TestClass testClass = new TestClass(new UserRepository(new AppDbContext()));
        return Ok(testClass.GetUsers());
    }
}