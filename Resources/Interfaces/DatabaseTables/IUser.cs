namespace Resources.Interfaces.DatabaseTables;

public interface IUser
{
    int Id { get; set; }
    string Email { get; set; }
    string PasswordHash { get; set; }
    bool IsSeller { get; set; }
    ICollection<IOrder> Orders { get; set; }
    ICollection<IAddress> Addresses { get; set; }
}