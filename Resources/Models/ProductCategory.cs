namespace Resources.Models;

public class ProductCategory : Entity
{
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [JsonIgnore]
    public Product Product { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [JsonIgnore]
    public Category Category { get; set; }
    // Navigation properties
}