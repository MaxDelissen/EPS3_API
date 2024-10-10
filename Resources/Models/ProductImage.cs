namespace Resources.Models;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [JsonIgnore]
    public Product Product { get; set; }

    [StringLength(255)]
    public string ImageLink { get; set; }

    // Navigation properties

}