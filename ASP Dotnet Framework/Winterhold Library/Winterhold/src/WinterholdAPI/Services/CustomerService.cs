using System.IO.Pipelines;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdAPI.Services;

public class CustomerService
{
    private readonly ICustomerRepository repository;

    public CustomerService(ICustomerRepository repository)
    {
        this.repository = repository;
    }

    public Customer GetCustomer(string memberNumber)
    {
        return repository.GetCustomerByNumber(memberNumber);
    }

    public List<Customer> GetCustomers()
    {
        return repository.GetAllCustomers();
    }
}
