namespace Resources.Models;

public class Category : Entity
{
    [Required, StringLength(255)]
    public string Name { get; set; }

    // Navigation properties
    /*public List<ProductCategory> ProductCategories { get; set; }*/
}