using Microsoft.AspNetCore.Mvc;
using Logic.Models;
using API.DTOs;
using Logic.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        private async Task<IActionResult> AddUser(UserDto userDto) //Switched to internal use only
        {
            var user = new User
            {
                email = userDto.Email,
                passwordHash = userDto.PasswordHash,
                isSeller = userDto.IsSeller
            };

            context.User.Add(user);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}