namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : Controller
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_productService.GetProducts());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

}