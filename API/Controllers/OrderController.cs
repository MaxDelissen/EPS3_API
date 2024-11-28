using Logic.Attributes;
using Resources.Exceptions;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrderController : ControllerBase
{
	private readonly OrderService _orderService;

	public OrderController(OrderService orderService)
	{
		_orderService = orderService;
	}

	[HttpGet("Seller")]
	[TokenValidation]
	public IActionResult GetSellerOrders(int sellerId)
	{
		try
		{
			var orders = _orderService.GetSellerOrders(sellerId);
			return orders.Count == 0 ? NotFound() : Ok(orders);
		}
		catch (NotFoundException)
		{
			return NotFound();
		}
		catch (Exception e)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}
}