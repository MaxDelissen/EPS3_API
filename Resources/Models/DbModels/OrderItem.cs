namespace Resources.Models.DbModels;

public class OrderItem : Entity
{
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    [JsonIgnore]
    public Order Order { get; set; }

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [JsonIgnore]
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; } // Price at the time of order

    // Navigation properties


}