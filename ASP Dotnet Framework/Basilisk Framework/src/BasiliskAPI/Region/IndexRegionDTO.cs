namespace BasiliskAPI.Region;

public class IndexRegionDTO
{
    public List<RegionDTO> RegionDTOs {get; set;}
    public string City { get; set; }
    public PaginationViewModel Pagination { get; set; }
    
}
