namespace Resources.DTOs;

public class OrderProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ThumbnailImage { get; set; }
    public int Quantity { get; set; }
}