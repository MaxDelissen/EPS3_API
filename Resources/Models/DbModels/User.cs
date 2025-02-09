namespace Resources.Models.DbModels;

public class User : Entity
{
    [StringLength(45)]
    public string FullName { get; set; }

    [StringLength(45)]
    public string Email { get; set; }

    [StringLength(255)]
    public string PasswordHash { get; set; }

    public bool IsSeller { get; set; }

    // Navigation properties
    public List<Order> Orders { get; set; }
    public List<Address> Addresses { get; set; }
}