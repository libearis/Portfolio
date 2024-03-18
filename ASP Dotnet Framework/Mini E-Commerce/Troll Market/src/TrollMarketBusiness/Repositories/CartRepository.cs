using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class CartRepository : ICartRepository
{
    private readonly TrollMarketContext dbContext;

    public CartRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Cart> GetAll(long buyerId, int pageNumber, int pageSize)
    {
        return dbContext.Carts.Include("Product").Include("Seller").Include("Shipment")
        .Where(cart => cart.BuyerId == buyerId)
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public List<Cart> GetAllPrice(long buyerId)
    {
        return dbContext.Carts.Include("Product").Include("Shipment")
        .Where(cart => cart.BuyerId == buyerId).ToList();
    }

    public Cart GetById(long id)
    {
        return dbContext.Carts.Find(id) ?? throw new NullReferenceException("ID keranjang tidak ada");
    }

    public int Count(long buyerId)
    {
        return dbContext.Carts.Include("Product").Include("Seller").Include("Shipment")
        .Where(cart => cart.BuyerId == buyerId)
        .Count();
    }

    public Cart? CheckExistingProduct(long productId, long shipmentId)
    {
        var model = dbContext.Carts.Where(cart => cart.ProductId == productId && cart.ShipmentId == shipmentId);
        if(model.Count() == 0)
        {
            return null;
        }

        return model.First();
    }

    public void Insert(Cart model)
    {
        dbContext.Carts.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Cart model)
    {
        dbContext.Carts.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Cart model)
    {
        dbContext.Carts.Remove(model);
        dbContext.SaveChanges();
    }
}
