namespace BasiliskWeb.ViewModels.Order;

public class OrderDetailVM
{
    public long Id { get; set; }
    public string InvoiceNumber { get; set; } = null!;
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get{
        return (int)Math.Ceiling((double)UnitPrice * Quantity);
    }}
}
