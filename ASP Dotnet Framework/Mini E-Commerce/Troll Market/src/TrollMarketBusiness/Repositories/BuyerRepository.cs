using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly TrollMarketContext dbContext;

    public BuyerRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Buyer GetById(long id)
    {
        return dbContext.Buyers.Find(id) ?? throw new NullReferenceException("ID Buyer tidak ada");
    }

    public Buyer GetByUsername(string username)
    {
        return dbContext.Buyers.First(buyer => buyer.Username == username) ?? throw new NullReferenceException("ID Buyer tidak ada");
    }

    public List<Buyer> GetAll()
    {
        return dbContext.Buyers.ToList();
    }

    public void Insert(Buyer model)
    {
        dbContext.Buyers.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Buyer model)
    {
        dbContext.Buyers.Update(model);
        dbContext.SaveChanges();
    }
}
