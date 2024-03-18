using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BasiliskTFContext dbContext;
    public CustomerRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }
    public int Count(string companyName, string contactName)
    {
        return dbContext.Customers.Where(
            cus => cus.CompanyName.Contains(companyName??"") &&
            cus.ContactPerson.Contains(contactName??"")
        ).Count();
    }


    public List<Customer> Get(int pageNumber, int pageSize, string companyName, string contactName)
    {
        return dbContext.Customers.Where(
            cus => cus.CompanyName.Contains(companyName??"") &&
            cus.ContactPerson.Contains(contactName??"")
        ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }


    public Customer GetById(long id)
    {
        return dbContext.Customers.Find(id)??throw new NullReferenceException($"Id {id} is not found");
    }

    public void Insert(Customer model)
    {
        dbContext.Customers.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Customer model)
    {
        dbContext.Customers.Update(model);
        dbContext.SaveChanges();
    }

    public void UpdateDelete(Customer model)
    {
        dbContext.Customers.Update(model);
        dbContext.SaveChanges();
    }
}
