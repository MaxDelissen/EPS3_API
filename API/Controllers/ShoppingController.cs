using System.Security.Claims;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingController : Controller
{
    private readonly ShoppingService _shoppingService;

    [HttpPost("Add")]
    public IActionResult AddToCart([FromBody] AddToCartRequest request)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !int.TryParse(userId, out _))
            return Unauthorized();

        try {
            _shoppingService.AddToCart(int.Parse(userId), request.ProductId, request.Quantity);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}