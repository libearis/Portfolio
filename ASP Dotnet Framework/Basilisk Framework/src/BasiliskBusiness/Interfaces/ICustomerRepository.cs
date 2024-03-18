using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface ICustomerRepository
{
    public Customer GetById(long id);
    public List<Customer> Get(int pageNumber, int pageSize, string companyName, string contactName);
    public int Count(string companyName, string contactName);
    public void Insert(Customer customer);
    public void Update(Customer customer);
    public void UpdateDelete(Customer customer);
}
