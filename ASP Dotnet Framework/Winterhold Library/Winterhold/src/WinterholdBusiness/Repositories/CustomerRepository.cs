using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly WinterholdContext dbContext;

    public CustomerRepository(WinterholdContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Customer GetCustomerByNumber(string memberNumber)
    {
        return dbContext.Customers.Find(memberNumber)?? throw new NullReferenceException("Customer Number is not exist");
    }

    public List<Customer> GetAllCustomers(int pageNumber, int pageSize, string memberNumber, string fullName, bool isExpired)
    {
        if(isExpired)
        {
            return dbContext.Customers.Where(cus => (cus.FirstName + " " + cus.LastName).Contains(fullName??"")&&
            cus.MembershipNumber.Contains(memberNumber??"") && cus.MembershipExpireDate < DateTime.Now)
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
        else
        {
            return dbContext.Customers.Where(cus => (cus.FirstName + " " + cus.LastName).Contains(fullName??"")&&
            cus.MembershipNumber.Contains(memberNumber??""))
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
    }

    public List<Customer> GetAllCustomers()
    {
        return dbContext.Customers.ToList();
    }

    public int CountAllCustomer(string memberNumber, string fullName, bool isExpired)
    {
        if(isExpired)
        {
            return dbContext.Customers.Where(cus => (cus.FirstName + " " + cus.LastName).Contains(fullName??"")&&
            cus.MembershipNumber.Contains(memberNumber??"") && cus.MembershipExpireDate < DateTime.Now)
            .Count();
        }
        else
        {
            return dbContext.Customers.Where(cus => (cus.FirstName + " " + cus.LastName).Contains(fullName??"")&&
            cus.MembershipNumber.Contains(memberNumber??""))
            .Count();
        }
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

    public void Delete(Customer model)
    {
        dbContext.Customers.Remove(model);
        dbContext.SaveChanges();
    }
}
