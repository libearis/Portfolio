using BasiliskWeb.ViewModels.Category;
using BasiliskWeb.ViewModels.Supplier;

namespace BasiliskWeb.ViewModels.Product;

public class IndexViewModel
{
    public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    public List<ProductCategoryVM> Categories { get; set; } = new List<ProductCategoryVM>();
    public List<ProductSupplierVM> Suppliers { get; set; } = new List<ProductSupplierVM>();
    public PaginationViewModel Pagination { get; set; }
    public string? ProductName { get; set; }
    public long CategoryId { get; set; }
    public long SupplierId { get; set; }
}
