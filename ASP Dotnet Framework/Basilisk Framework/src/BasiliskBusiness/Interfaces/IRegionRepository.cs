using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface IRegionRepository
{
    public Region GetById(long id);
    public List<Salesman> GetByRegionId(string employeeNumber, string fullName, string level, string superiorNumber, long id, int pageNumber, int pageSize);
    public List<Region> GetAllRegion(int pageNumber, int pageSize, string cityName);
    public int Count(int pageNumber, int pageSize, string cityName);
    public int CountDetail(string employeeNumber, string fullName, string level, string superiorName, long id);
    public void Insert(Region model);
    public void Update(Region model);
    public void Delete(Region model);
}
