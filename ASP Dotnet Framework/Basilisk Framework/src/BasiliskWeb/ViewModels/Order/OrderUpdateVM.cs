namespace BasiliskWeb.ViewModels.Order;

public class OrderUpdateVM
{
    public string InvoiceNumber { get; set; }
    public List<OrderCustomerVM> Customers { get; set; }
    public long CustomerId { get; set; }
    public List<OrderSalesmanVM> Salesmans { get; set; }
    public string SalesEmployeeNumber { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? ShippedDate { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; } = DateTime.Now;
    public List<OrderDeliveryVM> Deliveries { get; set; }
    public long DeliveryId { get; set; }
    public decimal DeliveryCost { get; set; }
    public string DestinationAddress { get; set; }
    public string DestinationCity { get; set; }
    public string DestinationPostalCode { get; set; }
}
