namespace Resources.Models;

public class Address : Entity
{
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    [StringLength(255)]
    public string Street { get; set; }

    [StringLength(255)]
    public string City { get; set; }

    [StringLength(255)]
    public string? State { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [StringLength(255)]
    public string Country { get; set; }

    // Navigation properties

    public List<Order> Orders { get; set; }
}