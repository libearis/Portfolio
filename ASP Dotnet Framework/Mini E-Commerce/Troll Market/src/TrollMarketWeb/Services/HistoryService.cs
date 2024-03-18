using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels.History;

namespace TrollMarketWeb.Services;

public class HistoryService
{
    private readonly IBuyerRepository buyerRepository;
    private readonly ISellerRepository sellerRepository;
    private readonly ITransactionRepository transactionRepository;

    public HistoryService(IBuyerRepository buyerRepository, ISellerRepository sellerRepository, ITransactionRepository transactionRepository)
    {
        this.buyerRepository = buyerRepository;
        this.sellerRepository = sellerRepository;
        this.transactionRepository = transactionRepository;
    }

    public List<SelectListItem> GetBuyers()
    {
        return buyerRepository.GetAll().Select(buyer => new SelectListItem()
        {
            Text = buyer.Name,
            Value = buyer.Id.ToString()
        }).ToList();
    }

    public List<SelectListItem> GetSellers()
    {
        return sellerRepository.GetAll().Select(seller => new SelectListItem()
        {
            Text = seller.Name,
            Value = seller.Id.ToString()
        }).ToList();
    }

    public IndexHistoryVM GetIndexHistoryVM(int pageNumber, long sellerId, long buyerId)
    {
        var model = transactionRepository.GetAll(pageNumber, Constant.PageSize, sellerId, buyerId).
        Select(tran => new HistoryTransactionVM()
        {
            Id = tran.Id,
            SellerName = tran.Seller.Name,
            BuyerName = tran.Buyer.Name,
            ProductName = tran.Product.Name,
            ShipmentName = tran.Shipment.Name,
            Date = tran.Date,
            Quantity = tran.Quantity,
            TotalPrice = tran.Product.UnitPrice * tran.Quantity + tran.Shipment.Price
        });

        return new IndexHistoryVM()
        {
            Histories = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = transactionRepository.CountAll(sellerId, buyerId)
            },
            Sellers = GetSellers(),
            SellerId = sellerId,
            Buyers = GetBuyers(),
            BuyerId = buyerId
        };
    }
}
