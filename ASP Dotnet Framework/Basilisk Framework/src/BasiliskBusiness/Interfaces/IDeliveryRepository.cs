using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface IDeliveryRepository
{
    public Delivery GetById(long id);
    public List<Delivery> Get(int pageNumber, int pageSize, string companyName);
    public int Count(string companyName);
    public void Insert(Delivery delivery);
    public void Update(Delivery delivery);
    public void Delete(Delivery delivery);
}
