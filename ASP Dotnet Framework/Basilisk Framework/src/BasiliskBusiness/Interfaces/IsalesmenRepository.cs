using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface ISalesmenRepository
{
    public Salesman GetByEmployeeNumber(string employeeNumber);
    public List<Salesman> GetAllSalesmen(int pageNumber, int pageSize);
    public int CountSales();
    
}
