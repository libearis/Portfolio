using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels.Shop;

namespace TrollMarketWeb.Services;

public class ShopService
{
    private readonly IShopRepository repository;
    private readonly IShipmentRepository shipmentRepository;

    public ShopService(IShopRepository repository, IShipmentRepository shipmentRepository)
    {
        this.repository = repository;
        this.shipmentRepository = shipmentRepository;
    }

    public List<SelectListItem> GetShipments()
    {
        var model = shipmentRepository.GetAll().Select(ship => new SelectListItem()
        {
            Text = ship.Name,
            Value = ship.Id.ToString()
        });

        return model.ToList();
    }

    public IndexShopVM GetIndexShopVM(int pageNumber, string productName, string categoryName, string description)
    {
        var model = repository.GetAll(pageNumber, Constant.PageSize, productName, categoryName, description)
        .Select(pro => new ProductVM()
        {
            Id = pro.Id,
            CategoryId = pro.CategoryId,
            CategoryName = pro.Category.Name,
            SellerId = pro.SellerId,
            SellerName = pro.Seller.Name,
            Name = pro.Name,
            Description = pro.Description,
            UnitPrice = pro.UnitPrice
        });

        return new IndexShopVM()
        {
            Products = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = repository.CountAll(productName, categoryName, description)
            },
            ShipmentOptions = GetShipments(),
            ProductName = productName,
            CategoryName = categoryName,
            Description = description
        };
    }
}
