using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class Category : ICategory
{
    public int Id { get; set; }

    [Required, StringLength(255)]
    public string Name { get; set; }

    // Navigation properties
    public ICollection<IProductCategory> ProductCategories { get; set; }
}