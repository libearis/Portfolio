using WinterholdAPI.BookDTOs;
using WinterholdBusiness.Interfaces;

namespace WinterholdAPI.Services;

public class BookService
{
    private readonly IBookRepository repository;
    private readonly IAuthorRepository authorRepository;

    public BookService(IBookRepository repository, IAuthorRepository authorRepository)
    {
        this.repository = repository;
        this.authorRepository = authorRepository;
    }

    public IndexBookDTO GetIndexBookVM(string categoryName, int pageNumber, int pageSize, string title, string author, bool IsAvailable)
    {
        var model = repository.GetBooksByCategory(categoryName, pageNumber, pageSize, title, author, IsAvailable).Select(book => new BookDTO()
        {
            Code = book.Code,
            Title = book.Title,
            CategoryName = categoryName,
            AuthorName = $"{authorRepository.GetById(book.AuthorId).Title} {authorRepository.GetById(book.AuthorId).FirstName} {authorRepository.GetById(book.AuthorId).LastName}",
            IsBorrowed = book.IsBorrowed,
            Summary = book.Summary,
            ReleaseDate = book.ReleaseDate,
            TotalPage = book.TotalPage
        });

        return new IndexBookDTO()
        {
            Books = model.ToList(),
            Pagination = new PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountBook(categoryName, title, author, IsAvailable)
            },
            Title = title,
            Author = author,
            CategoryName = categoryName
        };
    }
}
