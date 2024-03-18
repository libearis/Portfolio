namespace TrollMarketWeb.ViewModels.Shop;

public class ProductVM
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public string CategoryName { get; set; }
    public long SellerId { get; set; }
    public string SellerName { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int UnitPriceInt { get; set; }
    public bool Discontinue { get; set; }
    public bool IsPurchased { get; set; }
}
