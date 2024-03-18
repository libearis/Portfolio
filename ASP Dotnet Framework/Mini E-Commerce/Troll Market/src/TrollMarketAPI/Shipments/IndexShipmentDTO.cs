namespace TrollMarketAPI.Shipments;

public class IndexShipmentDTO
{
    public List<ShipmentDTO>? ShipmentDTOs { get; set; }
    public PaginationDTO? Pagination { get; set; }
}
