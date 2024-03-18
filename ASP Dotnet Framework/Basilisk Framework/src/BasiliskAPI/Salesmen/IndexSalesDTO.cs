using BasiliskAPI.Region;

namespace BasiliskAPI.Salesmen;

public class IndexSalesDTO
{
    public List<SalesDTO> SalesDTOs { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string EmployeeNumber { get; set; }
    public string FullName { get; set; }
    public string Level { get; set; }
    public string SuperiorName { get; set; }
}
