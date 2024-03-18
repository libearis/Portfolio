using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface IAccountRepository
{
    public Account GetAccount(string username);
    public void Insert(Account model);
    public void Update(Account model);
}
