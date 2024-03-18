using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface ILoanRepository
{
    public Loan GetLoanById(long id);
    public string GetBookTitle(string bookCode);
    public string GetCustomerName(string customerNumber);
    public List<Loan> GetAllLoans(int pageNumber, int pageSize, string bookName, string customerName, bool passedDueDate);
    public int Count(string bookName, string customerName, bool passedDueDate);
    public List<Book> GetAllBooks();
    public List<Customer> GetAllCustomers();
    public void Insert(Loan model);
    public void Update(Loan model);
    public void Delete(Loan model);
}
