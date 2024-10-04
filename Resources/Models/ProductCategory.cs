using System.ComponentModel.DataAnnotations;

namespace Resources.Models;

public class ProductCategory
{
    [Key]
    public int Id { get; set; } // Define primary key

    [Required]
    public int ProductId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    // Navigation properties
    public Product Product { get; set; }
    public Category Category { get; set; }
}