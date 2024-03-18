namespace WinterholdWeb.ViewModels.Author;

public class IndexAuthorVM
{
    public List<AuthorVM> Authors { get; set; }
    public PaginationVM Pagination { get; set; }
    public string Name { get; set; }
}
