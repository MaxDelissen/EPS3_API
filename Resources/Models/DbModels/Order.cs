namespace Resources.Models.DbModels;

public class Order : Entity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? ShippingAddressId { get; set; }
    [ForeignKey("ShippingAddressId")]
    public Address? ShippingAddress { get; set; }

    public int SellerId { get; set; }

    // Navigation properties
    public List<OrderItem> OrderItems { get; set; }
    public List<OrderStatusHistory> OrderStatusHistories { get; set; }
}