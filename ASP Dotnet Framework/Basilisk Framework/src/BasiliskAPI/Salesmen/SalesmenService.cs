using BasiliskBusiness.Interfaces;

namespace BasiliskAPI.Salesmen;

public class SalesmenService
{
    private readonly ISalesmenRepository repository;

    public SalesmenService(ISalesmenRepository repository)
    {
        this.repository = repository;
    }

    public string GetSalesFullName(string employeeNumber)
    {
        if(employeeNumber == null)
        {
            return " ";
        }
        else
        {
            return repository.GetByEmployeeNumber(employeeNumber).FirstName 
            + " " + repository.GetByEmployeeNumber(employeeNumber).LastName;
        }
    }

    public IndexSalesDTO GetAllSales(int pageNumber, int pageSize)
    {
        var model = repository.GetAllSalesmen(pageNumber, pageSize).Select(sal => new SalesDTO()
        {
            EmployeeNumber = sal.EmployeeNumber,
            FullName = $"{sal.FirstName} {sal.LastName}",
            Level = sal.Level,
            SuperiorEmployeeName = GetSalesFullName(sal.SuperiorEmployeeNumber)
        });

        return new IndexSalesDTO()
        {
            SalesDTOs = model.ToList(),
            Pagination = new Region.PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountSales()
            }
        };
    }
}
