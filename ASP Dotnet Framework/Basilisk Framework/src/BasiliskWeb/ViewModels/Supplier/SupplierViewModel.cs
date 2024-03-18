namespace BasiliskWeb.ViewModels.Supplier;

public class SupplierViewModel
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
}
