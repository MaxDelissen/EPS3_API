namespace Resources.Models;

public class OrderItem
{
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; } // Price at the time of order

    // Navigation properties
    public Order Order { get; set; }
    public Product Product { get; set; }
}