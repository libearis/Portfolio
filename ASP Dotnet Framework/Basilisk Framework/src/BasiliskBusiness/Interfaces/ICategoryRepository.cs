using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface ICategoryRepository
{
    public List<Category> Get();
    public List<Category> Get(int pageNumber, int pageSize, string name);
    public Category Get(long id);
    public int Count();
    public int Count(string name);
    public void Insert(Category category);
    public void Update(Category category);
    public void Delete(Category category);
}
