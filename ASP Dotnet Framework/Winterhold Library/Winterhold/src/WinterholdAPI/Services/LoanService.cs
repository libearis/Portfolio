using Microsoft.AspNetCore.Mvc.Rendering;
using WinterholdAPI.LoanDTOs;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdAPI.Services;

public class LoanService
{
    private readonly ILoanRepository repository;
    private readonly ICustomerRepository customerRepository;
    private readonly IBookRepository bookRepository;
    private readonly IAuthorRepository authorRepository;
    private readonly ICategoryRepository categoryRepository;

    public LoanService(ILoanRepository repository, ICustomerRepository customerRepository, IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
    {
        this.repository = repository;
        this.customerRepository = customerRepository;
        this.bookRepository = bookRepository;
        this.authorRepository = authorRepository;
        this.categoryRepository = categoryRepository;
    }

    public List<SelectListItem> GetAllCustomers()
    {
        var model = repository.GetAllCustomers().Select(cus => new SelectListItem()
        {
            Text = $"{cus.FirstName} {cus.LastName}",
            Value = cus.MembershipNumber
        });

        return model.ToList();
    }
    public List<SelectListItem> GetAllBooks()
    {
        var model = repository.GetAllBooks().Select(book => new SelectListItem()
        {
            Text = book.Title,
            Value = book.Code
        });

        return model.ToList();
    }

    public CustomerDTO GetCustomerDTO(string membershipNumber)
    {
        var model = customerRepository.GetCustomerByNumber(membershipNumber);
        return new CustomerDTO()
        {
            MembershipNumber = model.MembershipNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = model.Phone,
            MembershipExpireDate = model.MembershipExpireDate
        };
    }

    public BookDTO GetBookDTO(string bookCode)
    {
        var model = bookRepository.GetBooksByCode(bookCode);
        return new BookDTO()
        {
            Code = model.Code,
            Title = model.Title,
            CategoryName = model.CategoryName,
            AuthorName = $"{authorRepository.GetById(model.AuthorId).FirstName} {authorRepository.GetById(model.AuthorId).LastName}",
            Floor = categoryRepository.GetCategoryByName(model.CategoryName).Floor,
            Isle = categoryRepository.GetCategoryByName(model.CategoryName).Isle,
            Bay = categoryRepository.GetCategoryByName(model.CategoryName).Bay
        };
    }

    public IndexLoanDTO GetAllLoans(int pageNumber, int pageSize, string bookTitle, string customerName, bool passedDueDate)
    {
        var model = repository.GetAllLoans(pageNumber, pageSize, bookTitle, customerName, passedDueDate).Select(loan => new LoanDTO()
        {
            Id = loan.Id,
            Customer = GetCustomerDTO(loan.CustomerNumber),
            Book = GetBookDTO(loan.BookCode),
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnDate = loan.ReturnDate,
            Note = loan.Note
        });

        return new IndexLoanDTO()
        {
            LoanDTOs = model.ToList(),
            Pagination = new PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(bookTitle, customerName, passedDueDate)
            },
            BookTitle = bookTitle,
            CustomerName = customerName,
            PassedDueDate = passedDueDate
        };
    }

    public InsertLoanDTO GetInsertVM()
    {
        return new InsertLoanDTO()
        {
            Customers = GetAllCustomers(),
            Books = GetAllBooks()
        };
    }

    public UpdateLoanDTO GetUpdateVM(long id)
    {
        var model = repository.GetLoanById(id);
        return new UpdateLoanDTO()
        {
            Id = model.Id,
            Customers = GetAllCustomers(),
            Books = GetAllBooks(),
            CustomerNumber = model.CustomerNumber,
            BookCode = model.BookCode,
            LoanDate = model.LoanDate,
            Note = model.Note??""
        };
    }

    public void SetBookIsBorrowed(string bookCode, bool setIsBorrowed)
    {
        var model = bookRepository.GetBooksByCode(bookCode);
        model.IsBorrowed = setIsBorrowed;

        bookRepository.Update(model);
    }

    public void InsertNew(InsertLoanDTO viewModel)
    {
        var model = new Loan()
        {
            CustomerNumber = viewModel.CustomerNumber,
            BookCode = viewModel.BookCode,
            LoanDate = viewModel.LoanDate,
            DueDate = viewModel.LoanDate.AddDays(5),
            Note = viewModel.Note
        };

        SetBookIsBorrowed(model.BookCode, true);
        repository.Insert(model);
    }

    public void Update(UpdateLoanDTO viewModel)
    {
        var model = repository.GetLoanById(viewModel.Id);
        model.CustomerNumber = viewModel.CustomerNumber;
        SetBookIsBorrowed(model.BookCode, false);
        model.BookCode = viewModel.BookCode;
        SetBookIsBorrowed(model.BookCode, true);
        model.LoanDate = viewModel.LoanDate;
        model.Note = viewModel.Note;

        repository.Update(model);
    }

    public void Return(long id)
    {
        var model = repository.GetLoanById(id);
        model.ReturnDate = DateTime.Now;
        
        SetBookIsBorrowed(model.BookCode, false);
        repository.Update(model);
    }
}
