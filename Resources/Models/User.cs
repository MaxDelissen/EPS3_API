namespace Resources.Models;

public class User
{
    public int Id { get; set; }

    [Required, StringLength(45)]
    public string Email { get; set; }

    [Required, StringLength(255)]
    public string PasswordHash { get; set; }

    [Required]
    public bool IsSeller { get; set; }

    // Navigation properties
    public ICollection<Order> Orders { get; set; }
    public ICollection<Address> Addresses { get; set; }
}