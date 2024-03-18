namespace TrollMarketWeb.ViewModels.History;

public class HistoryTransactionVM
{
    public long Id { get; set; }
    public string SellerName { get; set; }
    public string BuyerName { get; set; }
    public string ProductName { get; set; }
    public string ShipmentName { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
