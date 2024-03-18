namespace BasiliskWeb.ViewModels.Order;

public class OrderDetailIndexVM
{
    public List<OrderDetailVM> OrderDetails { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string InvoiceNumber { get; set; }
    public string CustomerName { get; set; }
    public string SalesName { get; set; }
    public DateTime OrderDate { get; set; }
}
