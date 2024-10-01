using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class Product : IProduct
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
    public ICollection<IProductImage> ProductImages { get; set; }
    public ICollection<IOrderItem> OrderItems { get; set; }
    public ICollection<IProductCategory> ProductCategories { get; set; }
}