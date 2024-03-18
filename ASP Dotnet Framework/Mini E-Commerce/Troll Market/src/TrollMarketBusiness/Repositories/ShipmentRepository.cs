using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly TrollMarketContext dbContext;

    public ShipmentRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Shipment> GetAll()
    {
        return dbContext.Shipments.Where(ship => ship.Service == true).ToList();
    }

    public List<Shipment> GetAll(int pageNumber, int pageSize)
    {
        return dbContext.Shipments.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountAll()
    {
        return dbContext.Shipments.Count();
    }

    public Shipment GetById(long id)
    {
        return dbContext.Shipments.Find(id) ?? throw new NullReferenceException("Id Shipment tidak ada");
    }

    public bool IsUsed(long id)
    {
        return (dbContext.Transactions.Where(tran => tran.ShipmentId == id).Count() == 0? false : true) ||
        (dbContext.Carts.Where(cart => cart.ShipmentId == id).Count() == 0? false : true)? true : false;
    }

    public void Insert(Shipment model)
    {
        dbContext.Shipments.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Shipment model)
    {
        dbContext.Shipments.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Shipment model)
    {
        dbContext.Shipments.Remove(model);
        dbContext.SaveChanges();
    }
}
