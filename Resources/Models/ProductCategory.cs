namespace Resources.Models;

public class ProductCategory
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    // Navigation properties
    public Product Product { get; set; }
    public Category Category { get; set; }
}