using DAL;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class dbTestController : Controller
{
    private readonly UserRepository _userRepository;

    public dbTestController(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
    }


    [HttpGet]
    public IActionResult Get()
    {
        TestClass testClass = new TestClass(_userRepository);
        return Ok(testClass.GetUsers());
    }
}

