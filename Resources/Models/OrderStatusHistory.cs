namespace Resources.Models;

public class OrderStatusHistory
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    [JsonIgnore]
    public Order Order { get; set; }

    public byte Status { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation properties

}