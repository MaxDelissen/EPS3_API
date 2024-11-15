namespace Resources.Models.DbModels;

public class Product : Entity
{
    private decimal _price;

    [StringLength(255)]
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price
    {
        get => _price;
        set => _price = Math.Round(value, 2);
    }

    [StringLength(255)]
    public string ThumbnailImage { get; set; }

    public int? Stock { get; set; }

    // Navigation properties
    public List<ProductImage> ProductImages { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
}