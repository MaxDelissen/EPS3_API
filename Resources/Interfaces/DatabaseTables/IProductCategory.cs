namespace Resources.Interfaces.DatabaseTables;

public interface IProductCategory
{
    int ProductId { get; set; }
    int CategoryId { get; set; }
    IProduct Product { get; set; }
    ICategory Category { get; set; }
}
