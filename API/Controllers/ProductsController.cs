using System.Data;
using Logic.Attributes;
using Resources;
using Resources.DTOs;
using Resources.Exceptions;
using Resources.Models;
using Resources.Models.DbModels;

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
        try
        {
            var products = _productService.GetProduct();
            return products.Count == 0 ? NotFound() : Ok(products);
        }
        catch (DataException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet("Search")]
    public IActionResult Search([FromQuery] string query)
    {
        try
        {
            var products = _productService.GetProductByTitle(query);
            return products == null || products.Count == 0 ? NotFound() : Ok(products);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProductDto product) //Not yet implemented in frontend
    {
        try
        {
            Product newProduct = new Product
            {
                Title = product.Title,
                Description = product.Description ?? "",
                Price = product.Price,
                ThumbnailImage = product.ThumbnailImage,
                Stock = product.Stock ?? 0
            };
            _productService.AddProduct(newProduct);
            return Ok();
        }
        catch (InvalidLenghtException e) { return BadRequest(e.Message); }
        catch (DuplicateNameException e) { return Conflict(e.Message); }
        catch (FormatException e) { return BadRequest(e.Message); }
        catch (DataException e) { return StatusCode(500, e.Message); }
        catch (Exception e) { return StatusCode(500, e.Message); }
    }

    [HttpGet("stock/{id}")]
    public IActionResult GetStock(int id)
    {
        try
        {
            var stock = _productService.GetStock(id);
            if (stock == null)
                return NotFound();
            return Ok(stock);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}/stock/{stock}")]
    public  IActionResult UpdateStock(int id, int stock)
    {
        try
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            _productService.UpdateStock(product, stock);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            _productService.DeleteProduct(product);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [TokenValidation]
    [HttpGet("seller")]
    public IActionResult GetSellerProducts()
    {
        var simpleUser = HttpContext.Items["SimplifiedUser"] as SimpleUser;
        if (simpleUser != null && simpleUser.UserRole != UserRole.Seller)
            return Unauthorized();
        try
        {
            var products = _productService.GetUserProducts(simpleUser!.UserId);
            return products.Count == 0 ? NotFound() : Ok(products);
        }
        catch (DataException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }

    }
}