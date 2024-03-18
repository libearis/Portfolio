using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface ISupplierRepository
{
    public List<Supplier> Get();
    public List<Supplier> Get(int pageNumber, int pageSize);
    public List<Supplier> Get(int pageNumber, int pageSize, string companyName, string contactName, string jobTitle);
    public Supplier GetById(long id);
    public int Count();
    public int Count(string companyName, string contactName, string jobTitle);
    public void Insert(Supplier supplier);
    public void Update(Supplier supplier);
    public void Delete(Supplier supplier);
}
