namespace Resources.Interfaces.DatabaseTables;

public interface IAddress
{
    int Id { get; set; }
    int UserId { get; set; }
    string Street { get; set; }
    string City { get; set; }
    string State { get; set; }
    string PostalCode { get; set; }
    string Country { get; set; }
    IUser User { get; set; }
    ICollection<IOrder> Orders { get; set; }
}