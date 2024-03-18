namespace BasiliskWeb.ViewModels.Order;

public class IndexVM
{
    public List<OrderVM> Orders { get; set; } = new List<OrderVM>();
    public string InvoiceNumber { get; set; }
    public long CustomerId { get; set; }
    public string SalesEmployeeNumber { get; set; }
    public long DeliveryId { get; set; }
    public DateTime OrderDate { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public List<OrderCustomerVM> Customers { get; set; }
    public List<OrderSalesmanVM> Salesmans { get; set; }
    public List<OrderDeliveryVM> Deliveries { get; set; }
}
