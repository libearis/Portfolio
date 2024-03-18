using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly TrollMarketContext dbContext;

    public TransactionRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Transaction> GetAll(int pageNumber, int pageSize, long sellerId, long buyerId)
    {
        return dbContext.Transactions.Include("Seller").Include("Buyer")
        .Include("Shipment").Include("Product").Where(tran => (sellerId != 0? tran.SellerId == sellerId 
        : tran.SellerId != sellerId) && (buyerId != 0? tran.BuyerId == buyerId : tran.BuyerId != buyerId))
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountAll(long sellerId, long buyerId)
    {
        return dbContext.Transactions.Include("Seller").Include("Buyer")
        .Include("Shipment").Include("Product").Where(tran => sellerId != 0? tran.SellerId == sellerId 
        : tran.SellerId != sellerId && buyerId != 0? tran.BuyerId == buyerId : tran.BuyerId != buyerId)
        .Count();
    }

    public List<Transaction> GetByBuyerId(long id, int pageNumber, int pageSize)
    {
        return dbContext.Transactions
        .Include("Product")
        .Include("Shipment")
        .Where(tran => tran.BuyerId == id)
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountByBuyerId(long id)
    {
        return dbContext.Transactions
        .Include("Product")
        .Include("Shipment")
        .Where(tran => tran.BuyerId == id)
        .Count();
    }

    public Transaction GetById(long id)
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetBySellerId(long id, int pageNumber, int pageSize)
    {
        return dbContext.Transactions
        .Include("Product")
        .Include("Shipment")
        .Where(tran => tran.SellerId == id)
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountBySellerId(long id)
    {
        return dbContext.Transactions
        .Include("Product")
        .Include("Shipment")
        .Where(tran => tran.SellerId == id)
        .Count();
    }

    public void Insert(Transaction model)
    {
        dbContext.Transactions.Add(model);
        dbContext.SaveChanges();
    }
}
