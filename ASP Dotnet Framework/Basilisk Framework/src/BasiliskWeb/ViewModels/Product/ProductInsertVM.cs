namespace BasiliskWeb.ViewModels.Product;

public class ProductInsertVM
{
    public string? ProductName { get; set; }
    public List<ProductCategoryVM> Categories { get; set; } = new List<ProductCategoryVM>();
    public List<ProductSupplierVM> Suppliers { get; set; } = new List<ProductSupplierVM>();
    public long CategoryId { get; set; }
    public long SupplierId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int OnOrder { get; set; }
    public bool Discontinue { get; set; }
    public string Description { get; set; }
}
