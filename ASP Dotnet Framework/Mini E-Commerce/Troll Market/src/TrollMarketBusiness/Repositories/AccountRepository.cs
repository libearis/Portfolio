using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly TrollMarketContext dbContext;

    public AccountRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Account GetAccount(string username)
    {
        return dbContext.Accounts.Find(username)?? throw new NullReferenceException("Username tidak ada di database");
    }

    public void Insert(Account model)
    {
        dbContext.Accounts.Add(model);
        dbContext.SaveChanges();
    }
}
