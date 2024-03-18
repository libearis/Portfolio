namespace BasiliskAPI.Region;

public class IndexSalesDetailDTO
{
    public List<SalesDetailDTO> SalesDetailDTOs { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public long RegionId { get; set; }
    public string City { get; set; }
    public string Remark { get; set; }
    public string EmployeeNumber { get; set; }
    public string FullName { get; set; }
    public string Level { get; set; }
    public string SuperiorName { get; set; }
    public string SuperiorNumber { get; set; }
}
