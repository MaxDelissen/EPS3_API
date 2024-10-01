namespace Resources.Interfaces.DatabaseTables;

public interface IOrderStatusHistory
{
    int Id { get; set; }
    int OrderId { get; set; }
    byte Status { get; set; }
    DateTime UpdatedAt { get; set; }
    IOrder Order { get; set; }
}