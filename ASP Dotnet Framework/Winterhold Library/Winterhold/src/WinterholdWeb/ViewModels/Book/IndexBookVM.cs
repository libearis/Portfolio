namespace WinterholdWeb.ViewModels.Book;

public class IndexBookVM
{
    public string CategoryName { get; set; }
    public List<BookVM> Books { get; set; }
    public PaginationVM Pagination { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
}
