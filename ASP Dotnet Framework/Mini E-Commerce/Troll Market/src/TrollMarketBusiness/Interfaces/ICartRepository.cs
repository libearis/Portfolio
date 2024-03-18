using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface ICartRepository
{
    public List<Cart> GetAll(long buyerId, int pageNumber, int pageSize);
    public List<Cart> GetAllPrice(long buyerId);
    public Cart GetById(long id);
    public int Count(long buyerId);
    public Cart CheckExistingProduct(long productId, long shipmentId);
    public void Insert(Cart model);
    public void Update(Cart model);
    public void Delete(Cart model);
}
