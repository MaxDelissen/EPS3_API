namespace Resources.Models;

public class Product
{
    public int Id { get; set; }

    [Required, StringLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required, StringLength(255)]
    public string ThumbnailImage { get; set; }

    public int Stock { get; set; }

    // Navigation properties
    public List<ProductImage> ProductImages { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
}