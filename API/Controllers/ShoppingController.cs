using System.Security.Claims;
using Resources.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingController(ShoppingService shoppingService) : Controller
{
    [HttpPost("Add")]
    public IActionResult AddToCart([FromBody] AddToCartRequest request)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !int.TryParse(userId, out _))
            return Unauthorized();

        try {
            shoppingService.AddToCart(int.Parse(userId), request.ProductId, request.Quantity);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}