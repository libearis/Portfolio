using BasiliskBusiness.Interfaces;

namespace BasiliskAPI.Region;

public class RegionService
{
    private readonly IRegionRepository repositoryRegion;
    private readonly ISalesmenRepository repositorySales;

    public RegionService(IRegionRepository regionRepository, ISalesmenRepository salesmenRepository)
    {
        repositoryRegion = regionRepository;
        repositorySales = salesmenRepository;
    }

    public IndexRegionDTO GetAllRegion(int pageNumber, int pageSize, string cityName)
    {
        var model = repositoryRegion.GetAllRegion(pageNumber, pageSize, cityName).Select(reg=> new RegionDTO()
        {
            Id = reg.Id,
            City = reg.City,
            Remark = reg.Remark
        });
        
        return new IndexRegionDTO()
        {
            RegionDTOs = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repositoryRegion.Count(pageNumber, pageSize, cityName)
            }
        };
    }

    public void InsertNew(RegionDTO regionDTO)
    {
        var model = new TradeOfBasiliskDataAccess.Models.Region()
        {
            City = regionDTO.City,
            Remark = regionDTO.Remark
        };

        repositoryRegion.Insert(model);
    }

    public void Update(RegionDTO viewModel)
    {
        var model = repositoryRegion.GetById(viewModel.Id);
        model.City = viewModel.City;
        model.Remark = viewModel.Remark;

        repositoryRegion.Update(model);
    }

    public void Delete(long id)
    {
        var model = repositoryRegion.GetById(id);
        repositoryRegion.Delete(model);
    }

    public IndexSalesDetailDTO GetSalesmenDetail(string employeeNumber, string fullName, string level, string superiorName, long id, int pageNumber, int pageSize)
    {
        var model = repositoryRegion.GetByRegionId(employeeNumber, fullName, level, superiorName, id, pageNumber, pageSize);
        var listSales = new List<SalesDetailDTO>();
        foreach(var sales in model)
        {
            if(sales.SuperiorEmployeeNumber != null)
            {
                listSales.Add(new SalesDetailDTO()
                {
                    EmployeeNumber = sales.EmployeeNumber,
                    FullName = $"{sales.FirstName} {sales.LastName}",
                    Level = sales.Level,
                    SuperiorEmployeeName = repositorySales.GetByEmployeeNumber(sales.SuperiorEmployeeNumber).FirstName +
                    " " +  repositorySales.GetByEmployeeNumber(sales.SuperiorEmployeeNumber).LastName
                });
            }
            else
            {
                listSales.Add(new SalesDetailDTO()
                {
                    EmployeeNumber = sales.EmployeeNumber,
                    FullName = $"{sales.FirstName} {sales.LastName}",
                    Level = sales.Level,
                    SuperiorEmployeeName = ""
                });
            }
        }
        return new IndexSalesDetailDTO()
        {
            RegionId = id,
            SalesDetailDTOs = listSales,
            City = repositoryRegion.GetById(id).City,
            Remark = repositoryRegion.GetById(id).Remark??"",
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repositoryRegion.CountDetail(employeeNumber, fullName, level, superiorName, id)
            },
            EmployeeNumber = employeeNumber,
            FullName = fullName,
            Level = level,
            SuperiorName = superiorName
        };
    }
}
