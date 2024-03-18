using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IBuyerRepository
{
    public Buyer GetById(long id);
    public Buyer GetByUsername(string username);
    public List<Buyer> GetAll();
    public void Insert(Buyer model);
    public void Update(Buyer model);
}
