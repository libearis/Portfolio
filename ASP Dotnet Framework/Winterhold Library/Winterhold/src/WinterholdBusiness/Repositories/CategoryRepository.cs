using System.Data.Common;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly WinterholdContext dbContext;

    public CategoryRepository(WinterholdContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Category GetCategoryByName(string name)
    {
        return dbContext.Categories.Find(name)?? throw new NullReferenceException("Category Name is not found"); 
    }
    public List<Category> GetCategories(int pageNumber, int pageSize, string name)
    {
        return dbContext.Categories.Where(cat => cat.Name.Contains(name??""))
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountCategories(string name)
    {
        return dbContext.Categories.Where(cat => cat.Name.Contains(name??""))
        .Count();
    }

    public List<int> GetFloor()
    {
        return dbContext.Categories.Select(cat=>cat.Floor).Distinct().ToList();
    }

    public void Insert(Category model)
    {
        dbContext.Categories.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Category model)
    {
        dbContext.Categories.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Category model)
    {
        dbContext.Categories.Remove(model);
        dbContext.SaveChanges();
    }
}
