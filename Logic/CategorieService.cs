namespace Logic;

public class CategorieService(ICategorieRepository categorieRepository)
{
	public string GetCategorie(int id)
	{
		if (id == null || id == 0)
		{
			throw new ArgumentException("Id can't be null or 0");
		}
		var productCategory = categorieRepository.GetWhere(x => x.Id == id).FirstOrDefault();
		return productCategory.Category.Name;
	}
}