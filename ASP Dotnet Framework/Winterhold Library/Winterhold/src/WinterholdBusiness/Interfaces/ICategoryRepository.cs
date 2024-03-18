using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface ICategoryRepository
{
    public Category GetCategoryByName(string name);
    public List<Category> GetCategories(int pageNumber, int pageSize, string name);
    public int CountCategories(string name);
    public void Insert(Category model);
    public void Update(Category model);
    public void Delete(Category model);
}
