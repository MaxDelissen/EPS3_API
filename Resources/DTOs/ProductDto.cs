namespace Resources.DTOs;

public class ProductDto
{
    [StringLength(255)]
    public string Title { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    [StringLength(255)]
    public string ThumbnailImage { get; set; }

    public int? Stock { get; set; }
}