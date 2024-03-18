using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface ITransactionRepository
{
    public List<Transaction> GetAll(int pageNumber, int pageSize, long sellerId, long buyerId);
    public int CountAll(long seller, long buyer);
    public List<Transaction> GetByBuyerId(long id, int pageNumber, int pageSize);
    public int CountByBuyerId(long id);
    public List<Transaction> GetBySellerId(long id, int pageNumber, int pageSize);
    public int CountBySellerId(long id);
    public Transaction GetById(long id);
    public void Insert(Transaction model);
}
