namespace Resources.Models;

public class ProductImage : Entity
{
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [JsonIgnore]
    public Product Product { get; set; }

    [StringLength(255)]
    public string ImageLink { get; set; }

    // Navigation properties

}