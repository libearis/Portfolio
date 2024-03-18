namespace TrollMarketWeb.ViewModels.Cart;

public class CartVM
{
    public long Id { get; set; }
    public long BuyerId { get; set; }
    public string ProductName { get; set; }
    public string ShipmentName { get; set; }
    public string SellerName { get; set; }
    public long Quantity { get; set; }
    public long TotalPrice { get; set; }
}
