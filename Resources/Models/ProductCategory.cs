namespace Resources.Models;

public class ProductCategory
{
    [Key]
    public int Id { get; set; } // Define primary key

    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [JsonIgnore]
    public Product Product { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [JsonIgnore]
    public Category Category { get; set; }
    // Navigation properties


}