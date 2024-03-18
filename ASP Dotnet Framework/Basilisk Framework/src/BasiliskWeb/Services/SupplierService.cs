using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels.Supplier;
using BasiliskWeb.ViewModels;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Services;

public class SupplierService
{
    private readonly ISupplierRepository repository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        repository = supplierRepository;
    }

    public IndexViewModel Get(int pageNumber, int pageSize, string companyName, string contactName, string jobTitle)
    {
        var model = repository.Get(pageNumber, pageSize, companyName, contactName, jobTitle).Select(sup => new SupplierViewModel(){
            Id = sup.Id,
            CompanyName = sup.CompanyName,
            ContactPerson = sup.ContactPerson,
            JobTitle = sup.JobTitle
        });

        return new IndexViewModel()
        {
            Suppliers = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(companyName, contactName, jobTitle)
            },
            CompanyName = companyName,
            ContactName = contactName,
            JobTitle = jobTitle
        };
    }

    public SupplierUpdateVM GetById(long id)
    {
        try
        {
            var model = repository.GetById(id);
            return new SupplierUpdateVM()
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                ContactPerson = model.ContactPerson,
                JobTitle = model.JobTitle,
                Address = model.Address,
                City = model.City,
                Phone = model.Phone,
                Email = model.Email
            };
        }
        catch (System.Exception e)
        {
            throw new NullReferenceException(e.Message);
        }
    }

    public void Insert(SupplierInsertVM supplierInsertVM)
    {
        var model = new Supplier()
        {
            CompanyName = supplierInsertVM.CompanyName,
            ContactPerson = supplierInsertVM.ContactPerson,
            JobTitle = supplierInsertVM.JobTitle,
            Address = supplierInsertVM.Address,
            City = supplierInsertVM.City,
            Phone = supplierInsertVM.Phone,
            Email = supplierInsertVM.Email  
        };

        repository.Insert(model);
    }

    public void Update(SupplierUpdateVM viewModel)
    {
        var model = repository.GetById(viewModel.Id);
        model.CompanyName = viewModel.CompanyName;
        model.ContactPerson = viewModel.ContactPerson;
        model.JobTitle = viewModel.JobTitle;
        model.Address = viewModel.Address;
        model.City = viewModel.City;
        model.Phone = viewModel.Phone;
        model.Email = viewModel.Email;

        repository.Update(model);
    }

    public void SoftDelete(long id)
    {
        var supplier = repository.GetById(id);
        supplier.DeleteDate = DateTime.Now;
        repository.Update(supplier);
    }
}
