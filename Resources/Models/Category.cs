namespace Resources.Models;

public class Category
{
    public int Id { get; set; }

    [Required, StringLength(255)]
    public string Name { get; set; }

    // Navigation properties
    /*public List<ProductCategory> ProductCategories { get; set; }*/
}