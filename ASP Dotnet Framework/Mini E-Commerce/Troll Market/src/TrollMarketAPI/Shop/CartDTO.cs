namespace TrollMarketAPI.Shop;

public class CartDTO
{
    public string Username { get; set; }
    public long ProductId { get; set; }
    public long ShipmentId { get; set; }
    public int Quantity { get; set; }
}
