using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BasiliskTFContext dbContext;

    public AccountRepository(BasiliskTFContext dbContext)
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

    public void Update(Account model)
    {
        dbContext.Accounts.Update(model);
        dbContext.SaveChanges();
    }
}
