namespace BasiliskWeb.ViewModels.Order;

public class OrderDetailUpdateVM
{
    public long Id { get; set; }
    public string InvoiceNumber { get; set; } = null!;
    public List<OrderProductVM> Products { get; set; }
    public long ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
}
