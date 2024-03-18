using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface IProductRepository
{
    public Product GetById(long id);
    public List<Product> Get(int pageNumber, int pageSize);
    public List<Product> Get(long categoryId, long supplierId, int pageNumber, int pageSize, string productName);
    public Category GetCategory(long id);
    public List<Category> GetListCategory();
    public Supplier GetSupplier(long id);
    public List<Supplier> GetListSupplier();
    public int Count(long categoryId, long supplierId, string productName);
    public int Count();
    public void Insert(Product product);
    public void Update(Product product);
}
