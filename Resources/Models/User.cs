using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class User : IUser
{
    public int Id { get; set; }

    [Required, StringLength(45)]
    public string Email { get; set; }

    [Required, StringLength(255)]
    public string PasswordHash { get; set; }

    [Required]
    public bool IsSeller { get; set; }

    // Navigation properties
    public ICollection<IOrder> Orders { get; set; }
    public ICollection<IAddress> Addresses { get; set; }
}