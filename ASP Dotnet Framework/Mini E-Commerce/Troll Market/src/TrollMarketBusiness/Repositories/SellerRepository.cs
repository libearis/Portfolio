using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class SellerRepository : ISellerRepository
{
    private readonly TrollMarketContext dbContext;

    public SellerRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Seller GetById(long id)
    {
        return dbContext.Sellers.Find(id) ?? throw new NullReferenceException("ID Seller tidak ada");
    }

    public Seller GetByUsername(string username)
    {
        return dbContext.Sellers.Where(seller => seller.Username == username).First();
    }

    public List<Seller> GetAll()
    {
        return dbContext.Sellers.ToList();
    }

    public void Insert(Seller model)
    {
        dbContext.Sellers.Add(model);
        dbContext.SaveChanges();
    }
}
