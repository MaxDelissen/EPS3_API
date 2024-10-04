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
    public ICollection<ProductImage> ProductImages { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }
}