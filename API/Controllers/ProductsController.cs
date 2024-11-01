using System.Data;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Resources.DTOs;
using Resources.Exceptions;
using Resources.Models;

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
            return products == null || products.Count == 0 ? NotFound() : Ok(products);
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
}