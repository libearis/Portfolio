using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class SalesmenRepository : ISalesmenRepository
{
    private readonly BasiliskTFContext dbContext;

    public SalesmenRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }


    public Salesman GetByEmployeeNumber(string employeeNumber)
    {
        return dbContext.Salesmen.Find(employeeNumber)?? throw new NullReferenceException("Id sales tidak ada");
    }
    public List<Salesman> GetAllSalesmen(int pageNumber, int pageSize)
    {
        return dbContext.Salesmen.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountSales()
    {
        return dbContext.Salesmen.Count();
    }
}
