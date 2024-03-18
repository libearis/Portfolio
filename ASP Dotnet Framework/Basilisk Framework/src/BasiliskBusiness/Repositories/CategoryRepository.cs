using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BasiliskTFContext dbContext;
    public CategoryRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }

    public List<Category> Get()
    {
        var categories = dbContext.Categories;
        return categories.ToList();
    }

    public List<Category> Get(int pageNumber, int pageSize, string name)
    {
        return dbContext.Categories.Where(
            cat => cat.Name.Contains(name??"")
        ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public Category Get(long id)
    {
        return dbContext.Categories.Find(id)?? throw new NullReferenceException($"Id {id} is not found");
    }

    public int Count()
    {
        return dbContext.Categories.Count();
    }

    public int Count(string name)
    {
        return dbContext.Categories.Where(
            cat => cat.Name.Contains(name??"")
        ).Count();
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
