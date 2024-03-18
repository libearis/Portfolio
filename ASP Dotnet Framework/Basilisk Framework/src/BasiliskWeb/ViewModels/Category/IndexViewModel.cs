namespace BasiliskWeb.ViewModels.Category;

public class IndexViewModel
{
    public List<CategoryViewModel> Categories {get; set;} = new List<CategoryViewModel>();
    public PaginationViewModel Pagination { get; set; }
    public string Name { get; set; }
}
