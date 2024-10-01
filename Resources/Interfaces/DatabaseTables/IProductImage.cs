namespace Resources.Interfaces.DatabaseTables;

public interface IProductImage
{
    int Id { get; set; }
    int ProductId { get; set; }
    string ImageLink { get; set; }
    IProduct Product { get; set; }
}