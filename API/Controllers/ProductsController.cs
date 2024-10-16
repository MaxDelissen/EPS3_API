using System.Data;
using API.DTOs;
using Logic;
using Microsoft.AspNetCore.Mvc;
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
                Stock = product.Stock ?? 0 //Why does "??" look so funny to me? Like a confused smiley?
                                           //Or someone asking like product.Stock wháááát????
                                           //Maybe I didn't get enough sleep last night...
                                           //Should probably remove this comment before submitting...
                                           //Hey copilot, how ya doing with my autocompletes?
                                           //I'm doing great! I'm just a computer program, I don't need sleep!
                                           //Noice. Now im having conversations with the autocomoplete...
                                           //I'm not sure if that's a good thing...
                                           //I'm not sure either... I should probably stop...
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