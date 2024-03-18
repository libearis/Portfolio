namespace BasiliskWeb.ViewModels.Order;

public class OrderVM
{
    public string? InvoiceNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? SalesEmployeeNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string? DeliveryName { get; set; }
}
