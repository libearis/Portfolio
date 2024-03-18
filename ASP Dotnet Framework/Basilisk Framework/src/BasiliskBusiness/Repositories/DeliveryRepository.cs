using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly BasiliskTFContext dbContext;

    public DeliveryRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }

    public int Count(string companyName)
    {
        return dbContext.Deliveries.Where(
            del => del.CompanyName.Contains(companyName??"")
        ).Count();
    }


    public Delivery GetById(long id)
    {
        return dbContext.Deliveries.Find(id)?? throw new NullReferenceException($"Id {id} is not found");
    }
    public List<Delivery> Get(int pageNumber, int pageSize, string companyName)
    {
        return dbContext.Deliveries.Where(
            del => del.CompanyName.Contains(companyName??"")
        ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public void Insert(Delivery model)
    {
        dbContext.Deliveries.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Delivery model)
    {
        dbContext.Deliveries.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Delivery model)
    {
        dbContext.Deliveries.Remove(model);
        dbContext.SaveChanges();
    }
}
