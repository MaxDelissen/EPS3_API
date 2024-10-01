using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class Order : IOrder
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
    public IUser User { get; set; }
    public IAddress ShippingAddress { get; set; }
    public ICollection<IOrderItem> OrderItems { get; set; }
    public ICollection<IOrderStatusHistory> OrderStatusHistories { get; set; }
}