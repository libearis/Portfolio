using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class BookRepository : IBookRepository
{
    private readonly WinterholdContext dbContext;

    public BookRepository(WinterholdContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Book GetBooksByCode(string code)
    {
        return dbContext.Books.Find(code)?? throw new NullReferenceException("Book Code is not exist");
    }

    public int TotalBookBorrowedByAuthor(long id)
    {
        return dbContext.Books.Where(book => book.AuthorId == id && book.IsBorrowed == true).Count();
    }

    public int CountTotalBookByCategory(string name)
    {
        return dbContext.Books.Where(book => book.CategoryName == name).Count();
    }

    public List<Book> GetBooksByAuthor(long id)
    {
        return dbContext.Books.Where(book => book.AuthorId == id).ToList();
    }

    public List<Book> GetBooksByCategory(string name, int pageNumber, int pageSize, string title, string author, bool IsAvailable)
    {
        if(IsAvailable)
        {
            return dbContext.Books.Where(book => book.CategoryName == name && book.Title.Contains(title??"") &&
            (book.Author.Title + book.Author.FirstName + book.Author.LastName).Contains(author??"") && book.IsBorrowed == false)
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

        }
        else
        {
            return dbContext.Books.Where(book => book.CategoryName == name && book.Title.Contains(title??"") &&
            (book.Author.Title + book.Author.FirstName + book.Author.LastName).Contains(author??""))
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
    }

    public int CountBook(string name, string title, string author, bool IsAvailable)
    {
        if(IsAvailable)
        {
            return dbContext.Books.Where(book => book.CategoryName == name && book.Title.Contains(title??"") &&
            (book.Author.Title + book.Author.FirstName + book.Author.LastName).Contains(author??"") && book.IsBorrowed == false)
            .Count();
        }
        else
        {
            return dbContext.Books.Where(book => book.CategoryName == name && book.Title.Contains(title??"") &&
            (book.Author.Title + book.Author.FirstName + book.Author.LastName).Contains(author??""))
            .Count();
        }
    }

    public void Insert(Book model)
    {
        dbContext.Books.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Book model)
    {
        dbContext.Books.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Book model)
    {
        dbContext.Books.Remove(model);
        dbContext.SaveChanges();
    }
}
