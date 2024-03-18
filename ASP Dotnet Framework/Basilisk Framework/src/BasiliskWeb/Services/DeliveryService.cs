using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels.Delivery;
using BasiliskWeb.ViewModels;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Services;

public class DeliveryService
{
    private readonly IDeliveryRepository repository;

    public DeliveryService(IDeliveryRepository deliveryRepository)
    {
        repository = deliveryRepository;
    }

    public IndexViewModel Get(int pageNumber, int pageSize, string companyName)
    {
        var model = repository.Get(pageNumber, pageSize, companyName).Select(del => new DeliveryViewModel(){
            Id = del.Id,
            CompanyName = del.CompanyName,
            Phone = del.Phone,
            Cost = del.Cost
        });

        return new IndexViewModel()
        {
            Deliveries = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(companyName)
            },
            CompanyName = companyName
        };
    }

    public DeliveryUpdateVM GetById(long id)
    {
        var model = repository.GetById(id);
        return new DeliveryUpdateVM()
        {
            Id = model.Id,
            CompanyName = model.CompanyName,
            Phone = model.Phone,
            Cost = model.Cost
        };
    }

    public void Insert(DeliveryInsertVM deliveryInsertVM)
    {
        var model = new Delivery()
        {
            CompanyName = deliveryInsertVM.CompanyName,
            Phone = deliveryInsertVM.Phone,
            Cost = deliveryInsertVM.Cost
        };

        repository.Insert(model);
    }

    public void Update(DeliveryUpdateVM deliveryUpdateVM)
    {
        var model = repository.GetById(deliveryUpdateVM.Id);
        model.CompanyName = deliveryUpdateVM.CompanyName;
        model.Phone = deliveryUpdateVM.Phone;
        model.Cost = deliveryUpdateVM.Cost;

        repository.Update(model);
    }

    public void HardDelete(long id)
    {
        var model = repository.GetById(id);
        repository.Delete(model);
    }
}
