using System.Globalization;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels;
using WinterholdWeb.ViewModels.Author;
using WinterholdWeb.ViewModels.Book;

namespace WinterholdWeb.Services;

public class AuthorService
{
    private readonly IAuthorRepository repository;
    private readonly IBookRepository bookRepository;

    public AuthorService(IAuthorRepository repository, IBookRepository _bookRepository)
    {
        this.repository = repository;
        bookRepository = _bookRepository;
    }

    public IndexAuthorVM GetAllAuthor(int pageNumber, int pageSize, string name)
    {
        var model = repository.GetAllAuthor(pageNumber, pageSize, name).Select(aut => new AuthorVM()
        {
            Id = aut.Id,
            Title = aut.Title,
            FirstName = aut.FirstName,
            LastName = aut.LastName,
            BirthDate = aut.BirthDate,
            DeceasedDate = aut.DeceasedDate,
            Education = aut.Education,
            Summary = aut.Summary,
            TotalBookBorrowed = bookRepository.TotalBookBorrowedByAuthor(aut.Id)
        });
        return new IndexAuthorVM()
        {
            Authors = model.ToList(),
            Pagination = new PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountAllAuthor(name)
            },
            Name = name
        };
    }

    public void InsertNew(InsertAuthorVM viewModel)
    {
        var model = new Author()
        {
            Title = viewModel.Title,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate,
            DeceasedDate = viewModel.DeceasedDate,
            Education = viewModel.Education,
            Summary = viewModel.Summary
        };

        repository.InsertNew(model);
    }

    public UpdateAuthorVM GetUpdateModel(long id)
    {
        var model = repository.GetById(id);
        return new UpdateAuthorVM()
        {
            Id = model.Id,
            Title = model.Title,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            DeceasedDate = model.DeceasedDate,
            Education = model.Education,
            Summary = model.Summary
        };
    }

    public void Update(UpdateAuthorVM viewModel)
    {
        var model = repository.GetById(viewModel.Id);
        model.Title = viewModel.Title;
        model.FirstName = viewModel.FirstName;
        model.LastName = viewModel.LastName;
        model.BirthDate = viewModel.BirthDate;
        model.DeceasedDate = viewModel.DeceasedDate;
        model.Education = viewModel.Education;
        model.Summary = viewModel.Summary;

        repository.Update(model);
    }

    public IndexAuthorBooksVM GetBookDetails(long id)
    {
        var model = repository.GetById(id);
        var booksModel = bookRepository.GetBooksByAuthor(id).Select(book => new BookVM()
        {
            Title = book.Title,
            CategoryName = book.CategoryName,
            IsBorrowed = book.IsBorrowed,
            Summary = book.Summary,
            ReleaseDate = book.ReleaseDate,
            TotalPage = book.TotalPage
        });
        return new IndexAuthorBooksVM()
        {
            Author = new AuthorVM()
            {
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                DeceasedDate = model.DeceasedDate,
                Education = model.Education,
                Summary = model.Summary
            },
            Books = booksModel.ToList()
        };
    }

    public void Delete(long id)
    {
        var model = repository.GetById(id);
        repository.Delete(model);
    }
}
