using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface IBookRepository
{
    public Book GetBooksByCode(string code);
    public int TotalBookBorrowedByAuthor(long id);
    public List<Book> GetBooksByAuthor(long id);
    public List<Book> GetBooksByCategory(string categoryName, int pageNumber, int pageSize, string title, string author, bool IsAvailable);
    public int CountTotalBookByCategory(string name);
    public int CountBook(string categoryName, string title, string author, bool IsAvailable);
    public void Insert(Book model);
    public void Update(Book model);
    public void Delete(Book model);
}
