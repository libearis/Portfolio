namespace BasiliskWeb.ViewModels.Delivery;

public class IndexViewModel
{
    public List<DeliveryViewModel> Deliveries {get; set;} = new List<DeliveryViewModel>();
    public PaginationViewModel Pagination { get; set; }
    public string CompanyName { get; set; }
}
