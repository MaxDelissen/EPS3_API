using System.Security.Claims;
using Resources.DTOs;
using Resources.Exceptions;

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

    [HttpPut("Cart")]
    public IActionResult EditProductQuantity([FromBody] AddToCartRequest request)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !int.TryParse(userId, out _))
            return Unauthorized();

        try {
            shoppingService.EditProductQuantity(int.Parse(userId), request.ProductId, request.Quantity);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Cart")]
    public IActionResult GetCart()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !int.TryParse(userId, out _))
            return Unauthorized();

        try
        {
            var cart = shoppingService.GetCart(int.Parse(userId));
            return Ok(cart);
        } catch (ProductNotAvailableException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("Cart/{productId}")]
    public IActionResult RemoveFromCart(int productId)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !int.TryParse(userId, out _))
            return Unauthorized();

        try {
            shoppingService.RemoveFromCart(int.Parse(userId), productId);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}