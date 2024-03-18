using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IShopRepository
{
    public List<Product> GetAll(int pageNumber, int pageSize, string productName, string categoryName, string description);
    public List<Product> GetAllForSeller(long id, int pageNumber, int pageSize);
    public int CountAll(string productName, string categoryName, string description);
    public int CountAllForSeller(long id);
    public Product GetById(long id);
    public Category FindCategoryName(string name);
    public bool IsProductPurchased(long id);
    public void InsertCategory(Category model);
    public void Insert(Product model);
    public void Update(Product model);
    public void HardDelete(Product model);
}
