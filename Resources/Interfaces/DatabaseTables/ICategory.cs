namespace Resources.Interfaces.DatabaseTables;

public interface ICategory
{
    int Id { get; set; }
    string Name { get; set; }
    ICollection<IProductCategory> ProductCategories { get; set; }
}