namespace Resources.Interfaces.DatabaseTables;

public interface IOrder
{
    int Id { get; set; }
    int UserId { get; set; }
    byte Status { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    int ShippingAddressId { get; set; }
    IUser User { get; set; }
    IAddress ShippingAddress { get; set; }
    ICollection<IOrderItem> OrderItems { get; set; }
    ICollection<IOrderStatusHistory> OrderStatusHistories { get; set; }
}