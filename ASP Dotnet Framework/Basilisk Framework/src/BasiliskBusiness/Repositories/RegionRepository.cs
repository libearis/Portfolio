using BasiliskBusiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly BasiliskTFContext dbContext;

    public RegionRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }

    public Region GetById(long id)
    {
        return dbContext.Regions.Find(id)?? throw new NullReferenceException("Id region tidak ada");
    }

    public string GetSalesFullName(string invoiceNumber)
    {
        var fullName = dbContext.Salesmen.Find(invoiceNumber).FirstName + " " + dbContext.Salesmen.Find(invoiceNumber).LastName;
        return fullName;
    }

    public  List<Salesman> GetByRegionId(string employeeNumber, string fullName, string level, string superiorName, long id, int pageNumber, int pageSize)
    {
        return dbContext.Regions.Where(reg => reg.Id == id).SelectMany(sal => sal.SalesmanEmployeeNumbers)
        .Where(sal => sal.EmployeeNumber.Contains(employeeNumber??"")&&
        (sal.FirstName + " " + sal.LastName).Contains(fullName??"") &&
        sal.Level.Contains(level??"") 
        ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public List<Region> GetAllRegion(int pageNumber, int pageSize, string cityName)
    {
        return dbContext.Regions.Where(
            reg => reg.City.Contains(cityName??"")
        ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int Count(int pageNumber, int pageSize, string cityName)
    {
        return dbContext.Regions.Where(
            reg => reg.City.Contains(cityName??"")
        ).Count();
    }

    public int CountDetail(string employeeNumber, string fullName, string level, string superiorName, long id)
    {
        return dbContext.Regions.Where(reg => reg.Id == id).SelectMany(sal => sal.SalesmanEmployeeNumbers)
        .Where(sal => sal.EmployeeNumber.Contains(employeeNumber??"")&&
        (sal.FirstName + " " + sal.LastName).Contains(fullName??"") &&
        sal.Level.Contains(level??"")
        ).Count();
    }

    public void Insert(Region model)
    {
        dbContext.Regions.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Region model)
    {
        dbContext.Regions.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Region model)
    {
        dbContext.Regions.Remove(model);
        dbContext.SaveChanges();
    }
}
