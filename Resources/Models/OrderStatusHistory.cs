using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class OrderStatusHistory : IOrderStatusHistory
{
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public byte Status { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public IOrder Order { get; set; }
}