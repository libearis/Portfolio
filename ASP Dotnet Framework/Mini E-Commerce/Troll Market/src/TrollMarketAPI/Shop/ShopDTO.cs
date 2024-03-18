namespace TrollMarketAPI.Shop;

public class ShopDTO
{
    public long Id { get; set; }
    public string CategoryName { get; set; }
    public string SellerName { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public string Discontinue { get; set; }
}
