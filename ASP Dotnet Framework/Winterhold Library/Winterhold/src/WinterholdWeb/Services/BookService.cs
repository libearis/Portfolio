using Microsoft.AspNetCore.Mvc.Rendering;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels.Book;

namespace WinterholdWeb.Services;

public class BookService
{
    private readonly IBookRepository repository;
    private readonly IAuthorRepository authorRepository;

    public BookService(IBookRepository repository, IAuthorRepository _authorRepository)
    {
        this.repository = repository;
        authorRepository = _authorRepository;
    }

    public List<SelectListItem> GetAllAuthors()
    {
        var model = authorRepository.GetAllAuthor().Select(aut => new SelectListItem()
        {
            Text = $"{aut.FirstName} {aut.LastName}",
            Value = aut.Id.ToString()
        });

        return model.ToList();
    }

    public IndexBookVM GetIndexBookVM(string categoryName, int pageNumber, int pageSize, string title, string author, bool IsAvailable)
    {
        var model = repository.GetBooksByCategory(categoryName, pageNumber, pageSize, title, author, IsAvailable).Select(book => new BookVM()
        {
            Code = book.Code,
            Title = book.Title,
            CategoryName = categoryName,
            Author = new ViewModels.Author.AuthorVM()
            {
                Id = book.AuthorId,
                Title = authorRepository.GetById(book.AuthorId).Title,
                FirstName = authorRepository.GetById(book.AuthorId).FirstName,
                LastName = authorRepository.GetById(book.AuthorId).LastName,
            },
            IsBorrowed = book.IsBorrowed,
            Summary = book.Summary,
            ReleaseDate = book.ReleaseDate,
            TotalPage = book.TotalPage
        });

        return new IndexBookVM()
        {
            Books = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountTotalBookByCategory(categoryName)
            },
            Title = title,
            Author = author,
            CategoryName = categoryName
        };
    }

    public InsertBookVM GetInsertModel(string categoryName)
    {
        return new InsertBookVM()
        {
            CategoryName = categoryName,
            Authors = GetAllAuthors()
        };
    }

    public UpdateBookVM GetUpdateModel(string bookCode)
    {
        var model = repository.GetBooksByCode(bookCode);
        return new UpdateBookVM()
        {
            Code = model.Code,
            Title = model.Title,
            CategoryName = model.CategoryName,
            AuthorId = model.AuthorId,
            Authors = GetAllAuthors(),
            ReleaseDate = model.ReleaseDate,
            TotalPage = model.TotalPage,
            Summary = model.Summary
        };
    }

    public void Insert(InsertBookVM viewModel)
    {
        var model = new Book()
        {
            Code = viewModel.Code,
            Title = viewModel.Title,
            CategoryName = viewModel.CategoryName,
            AuthorId = viewModel.AuthorId,
            ReleaseDate = viewModel.ReleaseDate,
            TotalPage = viewModel.TotalPage,
            Summary = viewModel.Summary
        };

        repository.Insert(model);
    }

    public void Update(UpdateBookVM viewModel)
    {
        var model = repository.GetBooksByCode(viewModel.Code);
        model.Code = viewModel.Code;
        model.Title = viewModel.Title;
        model.AuthorId = viewModel.AuthorId;
        model.ReleaseDate = viewModel.ReleaseDate;
        model.TotalPage = viewModel.TotalPage;
        model.Summary = viewModel.Summary;

        repository.Update(model); 
    }

    public void Delete(string bookCode)
    {
        var model = repository.GetBooksByCode(bookCode);
        repository.Delete(model);
    }
}
