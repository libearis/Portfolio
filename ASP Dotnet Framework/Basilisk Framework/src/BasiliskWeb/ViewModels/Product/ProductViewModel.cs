namespace BasiliskWeb.ViewModels.Product;

public class ProductViewModel
{
    public long Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string CategoryName { get; set; }
    public string SupplierName { get; set; }
    public decimal Price { get; set; }
}
