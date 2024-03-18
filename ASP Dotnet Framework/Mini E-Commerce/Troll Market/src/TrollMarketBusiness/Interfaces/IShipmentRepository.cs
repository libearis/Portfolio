using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IShipmentRepository
{
    public List<Shipment> GetAll();
    public List<Shipment> GetAll(int pageNumber, int pageSize);
    public int CountAll();
    public Shipment GetById(long id);
    public bool IsUsed(long id);
    public void Insert(Shipment model);
    public void Update(Shipment model);
    public void Delete(Shipment model);
}
