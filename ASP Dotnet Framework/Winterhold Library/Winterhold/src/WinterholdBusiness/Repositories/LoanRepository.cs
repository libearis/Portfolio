using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly WinterholdContext dbContext;

    public LoanRepository(WinterholdContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Loan GetLoanById(long id)
    {
        return dbContext.Loans.Find(id)?? throw new NullReferenceException("Loan ID is not exist");
    }
    
    public string GetBookTitle(string bookCode)
    {
        return dbContext.Books.Find(bookCode).Title;
    }

    public string GetCustomerName(string customerNumber)
    {
        return $"{dbContext.Customers.Find(customerNumber).FirstName} {dbContext.Customers.Find(customerNumber).LastName}";
    }

    public List<Loan> GetAllLoans(int pageNumber, int pageSize, string bookName, string customerName, bool passedDueDate)
    {
        if(passedDueDate)
        {
            return dbContext.Loans.Where(loan => loan.BookCodeNavigation.Title.Contains(bookName??"") &&
            (loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName).Contains(customerName??"") &&
            loan.DueDate < DateTime.Now && loan.ReturnDate == null)
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
        else
        {
            return dbContext.Loans.Where(loan => loan.BookCodeNavigation.Title.Contains(bookName??"") &&
            (loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName).Contains(customerName??""))
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
    }

    public int Count(string bookName, string customerName, bool passedDueDate)
    {
        if(passedDueDate)
        {
            return dbContext.Loans.Where(loan => loan.BookCodeNavigation.Title.Contains(bookName??"") &&
            (loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName).Contains(customerName??"") &&
            loan.DueDate < DateTime.Now && loan.ReturnDate == null)
            .Count();
        }
        else
        {
            return dbContext.Loans.Where(loan => loan.BookCodeNavigation.Title.Contains(bookName??"") &&
            (loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName).Contains(customerName??""))
            .Count();
        }
    }

    public void Insert(Loan model)
    {
        dbContext.Loans.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Loan model)
    {
        dbContext.Loans.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Loan model)
    {
        dbContext.Loans.Remove(model);
        dbContext.SaveChanges();
    }

    public List<Book> GetAllBooks()
    {
        return dbContext.Books.Where(book => book.IsBorrowed == false).ToList();
    }

    public List<Customer> GetAllCustomers()
    {
        return dbContext.Customers.Where(cus => cus.MembershipExpireDate >= DateTime.Now).ToList();
    }
}
