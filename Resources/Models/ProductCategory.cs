using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class ProductCategory : IProductCategory
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    // Navigation properties
    public IProduct Product { get; set; }
    public ICategory Category { get; set; }
}