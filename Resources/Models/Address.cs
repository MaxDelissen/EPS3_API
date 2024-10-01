using Resources.Interfaces.DatabaseTables;

namespace Resources.Models;

public class Address : IAddress
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [StringLength(255)]
    public string Street { get; set; }

    [StringLength(255)]
    public string City { get; set; }

    [StringLength(255)]
    public string State { get; set; }

    [StringLength(20)]
    public string PostalCode { get; set; }

    [StringLength(255)]
    public string Country { get; set; }

    // Navigation properties
    public IUser User { get; set; }
    public ICollection<IOrder> Orders { get; set; }
}