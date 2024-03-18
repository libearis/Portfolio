namespace WinterholdWeb.ViewModels.Customer;

public class IndexCustomerVM
{
    public List<CustomerVM> Customers { get; set; }
    public PaginationVM Pagination { get; set; }
    public string MemberNumber { get; set; }
    public string CustomerName { get; set; }
    public bool IsExpired { get; set; }
}
