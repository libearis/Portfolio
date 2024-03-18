namespace TrollMarketWeb.ViewModels.Cart;

public class TransactionVM
{
    public long BuyerId { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal RemainingBalance { get; set; }
}
