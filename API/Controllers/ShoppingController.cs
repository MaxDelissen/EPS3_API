using System.Security.Claims;
using Resources.DTOs;
using Resources.Exceptions;
using Resources.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingController(ShoppingService shoppingService) : Controller
{
    [HttpPost("Add")]
    [TokenValidation]
    public IActionResult AddToCart([FromBody] AddToCartRequest request)
    {
        var simpleUser = HttpContext.Items["SimplifiedUser"] as SimpleUser;

        try {
            shoppingService.AddToCart(simpleUser.UserId, request.ProductId, request.Quantity);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Cart")]
    [TokenValidation]
    public IActionResult EditProductQuantity([FromBody] AddToCartRequest request)
    {
        int userId = (HttpContext.Items["SimplifiedUser"] as SimpleUser).UserId;

        try {
            shoppingService.EditProductQuantity(userId, request.ProductId, request.Quantity);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Cart")]
    [TokenValidation]
    public IActionResult GetCart()
    {
        int userId = (HttpContext.Items["SimplifiedUser"] as SimpleUser).UserId;

        try
        {
            var cart = shoppingService.GetCart(userId);
            return Ok(cart);
        } catch (ProductNotAvailableException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("Cart/{productId}")]
    [TokenValidation]
    public IActionResult RemoveFromCart(int productId)
    {
        int userId = (HttpContext.Items["SimplifiedUser"] as SimpleUser).UserId;

        try {
            shoppingService.RemoveFromCart(userId, productId);
            return Ok();
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
}