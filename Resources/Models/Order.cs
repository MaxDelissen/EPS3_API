namespace Resources.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public byte Status { get; set; }  // Use byte for tinyint

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    [Required]
    public int ShippingAddressId { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Address ShippingAddress { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
}