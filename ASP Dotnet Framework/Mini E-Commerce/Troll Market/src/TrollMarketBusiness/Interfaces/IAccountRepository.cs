using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IAccountRepository
{
    public Account GetAccount(string username);
    public void Insert(Account model);
}
