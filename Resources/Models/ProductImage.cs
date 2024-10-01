namespace Resources.Models;

public class ProductImage
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required, StringLength(255)]
    public string ImageLink { get; set; }

    // Navigation properties
    public Product Product { get; set; }
}