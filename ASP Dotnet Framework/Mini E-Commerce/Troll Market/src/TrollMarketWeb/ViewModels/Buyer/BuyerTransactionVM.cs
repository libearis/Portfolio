namespace TrollMarketWeb.ViewModels.Buyer;

public class BuyerTransactionVM
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public string ShipmentName { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
