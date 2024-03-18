namespace TrollMarketAPI.Shipments;

public class ShipmentDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public bool Service { get; set; }
    public bool IsUsed { get; set; }
}
