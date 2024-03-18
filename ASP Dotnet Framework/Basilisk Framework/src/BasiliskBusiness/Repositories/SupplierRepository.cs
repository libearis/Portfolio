using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly BasiliskTFContext dbContext;
    public SupplierRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }

    public List<Supplier> Get()
    {
        var suppliers = dbContext.Suppliers;
        return suppliers.ToList();
    }

    public List<Supplier> Get(int pageNumber, int pageSize)
    {
        return dbContext.Suppliers.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public List<Supplier> Get(int pageNumber, int pageSize, string companyName, string contactName, string jobTitle)
    {
        return dbContext.Suppliers.Where(
            sup => sup.CompanyName.Contains(companyName??"") &&
            sup.ContactPerson.Contains(contactName??"") &&
            sup.JobTitle.Contains(jobTitle??"")
        ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public Supplier GetById(long id)
    {
        return dbContext.Suppliers.Find(id)?? throw new NullReferenceException($"Id {id} is not found");
    }

    public int Count()
    {
        return dbContext.Suppliers.Count();
    }

    public int Count(string companyName, string contactName, string jobTitle)
    {
        return dbContext.Suppliers.Where(
            sup => sup.CompanyName.Contains(companyName??"") &&
            sup.ContactPerson.Contains(contactName??"") &&
            sup.JobTitle.Contains(jobTitle??"")
        ).Count();
    }

    public void Insert(Supplier model)
    {
        dbContext.Suppliers.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Supplier model)
    {
        dbContext.Suppliers.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Supplier model)
    {
        dbContext.Suppliers.Remove(model);
        dbContext.SaveChanges();
    }
}
