namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CategoriesController : Controller
{
	private readonly CategorieService _categoriesService;

	public CategoriesController(CategorieService categorieService)
	{
		_categoriesService = categorieService;
	}

	/// <summary>
	/// Get's the name of the category by id
	/// </summary>
	/// <returns></returns>
	[HttpGet ("{id}")]
	public IActionResult GetCategoryName(int id)
	{
		try
		{
			var category = _categoriesService.GetCategorie(id);
			return category == null ? NotFound() : Ok(category);
		}
		catch (Exception e)
		{
			return StatusCode(500, e.Message);
		}
	}
}