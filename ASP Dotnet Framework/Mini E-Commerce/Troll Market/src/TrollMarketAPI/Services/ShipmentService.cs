using TrollMarketAPI.Shipments;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Services;

public class ShipmentService
{
    private readonly IShipmentRepository repository;

    public ShipmentService(IShipmentRepository repository)
    {
        this.repository = repository;
    }

    public IndexShipmentDTO GetIndexShipmentDTO(int pageNumber)
    {
        var model = repository.GetAll(pageNumber, Constant.PageSize).Select(ship => new ShipmentDTO()
        {
            Id = ship.Id,
            Name = ship.Name,
            Price = ship.Price,
            Service = ship.Service,
            IsUsed = repository.IsUsed(ship.Id)
        });

        return new IndexShipmentDTO()
        {
            ShipmentDTOs = model.ToList(),
            Pagination = new PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = repository.CountAll()
            }
        };
    }

    public void InsertNew(ShipmentDTO shipmentDTO)
    {
        var model = new Shipment()
        {
            Name = shipmentDTO.Name,
            Price = shipmentDTO.Price,
            Service = shipmentDTO.Service
        };

        repository.Insert(model);
    }

    public void Edit(ShipmentDTO shipmentDTO)
    {
        var model = repository.GetById(shipmentDTO.Id);
        model.Name = shipmentDTO.Name;
        model.Price = shipmentDTO.Price;
        model.Service = shipmentDTO.Service;

        repository.Update(model);
    }

    public void StopService(long id)
    {
        var model = repository.GetById(id);
        model.Service = false;

        repository.Update(model);
    }

    public void Delete(long id)
    {
        var model = repository.GetById(id);
        repository.Delete(model);
    }
}
