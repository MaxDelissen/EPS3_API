namespace Resources.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    [Required]
    public int ShippingAddressId { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Address ShippingAddress { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public List<OrderStatusHistory> OrderStatusHistories { get; set; }
}