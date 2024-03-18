using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface ISellerRepository
{
    public Seller GetById(long id);
    public Seller GetByUsername(string username);
    public List<Seller> GetAll();
    public void Insert(Seller model);
}
