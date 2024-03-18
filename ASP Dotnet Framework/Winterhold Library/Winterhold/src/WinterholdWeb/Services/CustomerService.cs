using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels.Customer;

namespace WinterholdWeb.Services;

public class CustomerService
{
    private readonly ICustomerRepository repository;

    public CustomerService(ICustomerRepository repository)
    {
        this.repository = repository;
    }

    public IndexCustomerVM GetIndexVM(int pageNumber, int pageSize, string memberNumber, string customerName, bool isExpired)
    {
        var model = repository.GetAllCustomers(pageNumber, pageSize, memberNumber, customerName, isExpired).Select(cus => new CustomerVM()
        {
            MembershipNumber = cus.MembershipNumber,
            FirstName = cus.FirstName,
            LastName = cus.LastName,
            BirthDate = cus.BirthDate,
            Gender = cus.Gender,
            Phone = cus.Phone,
            Address = cus.Address,
            MembershipExpireDate = cus.MembershipExpireDate
        });

        return new IndexCustomerVM()
        {
            Customers = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountAllCustomer(memberNumber, customerName, isExpired)
            },
            MemberNumber = memberNumber,
            CustomerName = customerName,
            IsExpired = isExpired
        };
    }

    public void InsertNewCustomer(InsertCustomerVM viewModel)
    {
        var model = new Customer()
        {
            MembershipNumber = viewModel.MembershipNumber,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate,
            Gender = viewModel.Gender,
            Phone = viewModel.Phone,
            Address = viewModel.Address,
            MembershipExpireDate = DateTime.Now.AddYears(2)
        };

        repository.Insert(model);
    }

    public UpdateCustomerVM GetUpdateVM(string memberNumber)
    {
        var model = repository.GetCustomerByNumber(memberNumber);

        return new UpdateCustomerVM()
        {   
            MembershipNumber = model.MembershipNumber,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Gender = model.Gender,
            Phone = model.Phone,
            Address = model.Address
        };
    }

    public void Update(UpdateCustomerVM viewModel)
    {
        var model = repository.GetCustomerByNumber(viewModel.MembershipNumber);
        model.FirstName = viewModel.FirstName;
        model.LastName = viewModel.LastName;
        model.BirthDate = viewModel.BirthDate;
        model.Gender = viewModel.Gender;
        model.Phone = viewModel.Phone;
        model.Address = viewModel.Address;

        repository.Update(model);
    }

    public void Delete(string memberNumber)
    {
        var model = repository.GetCustomerByNumber(memberNumber);
        repository.Delete(model);
    }

    public void Extend(string memberNumber)
    {
        var model = repository.GetCustomerByNumber(memberNumber);
        model.MembershipExpireDate = model.MembershipExpireDate.AddYears(2);

        repository.Update(model);
    }
}
