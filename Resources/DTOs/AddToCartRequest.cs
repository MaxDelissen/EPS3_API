namespace Resources.DTOs;

public class AddToCartRequest
{
    [Required]
    public int ProductId { get; set; }
    public int? Quantity { get; set; } // Default to 1
}