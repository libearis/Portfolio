using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels.Customer;
using BasiliskWeb.ViewModels;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Services;

public class CustomerService
{
    private readonly ICustomerRepository repository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        repository = customerRepository;
    }

    public IndexViewModel Get(int pageNumber, int pageSize, string companyName, string contactPerson)
    {
        var model = repository.Get(pageNumber, pageSize, companyName, contactPerson).Select(cus => new CustomerViewModel(){
            Id = cus.Id,
            CompanyName = cus.CompanyName,
            ContactPerson = cus.ContactPerson,
            Address = cus.Address,
            City = cus.City
        });

        return new IndexViewModel()
        {
            Customers = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(companyName, contactPerson)
            },
            CompanyName = companyName,
            ContactPerson = contactPerson
        };
    }

    public CustomerUpdateVM GetById(long id)
    {
        var model = repository.GetById(id);
        return new CustomerUpdateVM()
        {
            CompanyName = model.CompanyName,
            ContactPerson = model.ContactPerson,
            Address = model.Address,
            City = model.City,
            Phone = model.Phone,
            Email = model.Email
        };
    }

    public void Insert(CustomerInsertVM viewModel)
    {
        var model = new Customer()
        {
            CompanyName = viewModel.CompanyName,
            ContactPerson = viewModel.ContactPerson,
            Address = viewModel.Address,
            City = viewModel.City,
            Phone = viewModel.Phone,
            Email = viewModel.Email
        };

        repository.Insert(model);
    }
    
    public void SoftDelete(long id)
    {
        var model = repository.GetById(id);
        model.DeleteDate = DateTime.Now;
        repository.UpdateDelete(model);
    }

    public void Update(CustomerUpdateVM viewModel)
    {
        var model = repository.GetById(viewModel.Id);
        model.CompanyName = viewModel.CompanyName;
        model.ContactPerson = viewModel.ContactPerson;
        model.Address = viewModel.Address;
        model.City = viewModel.City;
        model.Phone = viewModel.Phone;
        model.Email = viewModel.Email;

        repository.Update(model);
    }
}
