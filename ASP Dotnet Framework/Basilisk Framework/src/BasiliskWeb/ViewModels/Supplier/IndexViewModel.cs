namespace BasiliskWeb.ViewModels.Supplier;

public class IndexViewModel
{
    public List<SupplierViewModel> Suppliers { get; set; } = new List<SupplierViewModel>();
    public PaginationViewModel Pagination { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string JobTitle { get; set; }
}
