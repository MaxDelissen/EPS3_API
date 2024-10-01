using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class ProductImage : IProductImage
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required, StringLength(255)]
    public string ImageLink { get; set; }

    // Navigation properties
    public IProduct Product { get; set; }
}