using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface ICustomerRepository
{
    public Customer GetCustomerByNumber(string memberNumber);
    public List<Customer> GetAllCustomers(int pageNumber, int pageSize, string memberNumber, string fullName, bool isExpired);
    public List<Customer> GetAllCustomers();
    public int CountAllCustomer(string memberNumber, string fullName, bool isExpired);
    public void Insert(Customer model);
    public void Update(Customer model);
    public void Delete(Customer model);
}
