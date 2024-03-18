using WinterholdWeb.ViewModels.Book;

namespace WinterholdWeb.ViewModels.Author;

public class IndexAuthorBooksVM
{
    public AuthorVM Author { get; set; }
    public List<BookVM> Books { get; set; } = new List<BookVM>();
}
