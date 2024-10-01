namespace Resources.Interfaces.DatabaseTables;

public interface IProduct
{
    int Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    string ThumbnailImage { get; set; }
    int Stock { get; set; }
    ICollection<IProductImage> ProductImages { get; set; }
    ICollection<IOrderItem> OrderItems { get; set; }
    ICollection<IProductCategory> ProductCategories { get; set; }
}