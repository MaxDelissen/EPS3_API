namespace Resources.DTOs;

public class OrderDto
{
    public int UserId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? ShippingAddressId { get; set; }
}