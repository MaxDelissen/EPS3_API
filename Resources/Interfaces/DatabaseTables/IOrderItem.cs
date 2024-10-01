namespace Resources.Interfaces.DatabaseTables;

public interface IOrderItem
{
    int Id { get; set; }
    int OrderId { get; set; }
    int ProductId { get; set; }
    int Quantity { get; set; }
    decimal Price { get; set; }
    IOrder Order { get; set; }
    IProduct Product { get; set; }
}