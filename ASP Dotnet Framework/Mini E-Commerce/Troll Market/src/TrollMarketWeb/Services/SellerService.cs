using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Account;
using TrollMarketWeb.ViewModels.Seller;

namespace TrollMarketWeb.Services;

public class SellerService
{
    private readonly ISellerRepository repository;
    private readonly ITransactionRepository transactionRepository;

    public SellerService(ISellerRepository repository, ITransactionRepository transactionRepository)
    {
        this.repository = repository;
        this.transactionRepository = transactionRepository;
    }

    public void InsertNew(RegisterVM viewModel)
    {
        var model = new Seller()
        {
            Username = viewModel.Username,
            Name = viewModel.Name,
            Address = viewModel.Address,
        };

        repository.Insert(model);
    }

    public SellerVM GetSellerVM(string username, int pageNumber)
    {
        var model = repository.GetByUsername(username);
        var transactionsModel = transactionRepository.GetBySellerId(model.Id, pageNumber, Constant.PageSize).Select(tran => new SellerTransactionVM()
        {
            Id = tran.Id,
            ProductName = tran.Product.Name,
            ShipmentName = tran.Shipment.Name,
            Date = tran.Date,
            Quantity = tran.Quantity,
            TotalPrice = tran.Product.UnitPrice * tran.Quantity + tran.Shipment.Price
        });

        return new SellerVM()
        {
            Id = model.Id,
            UserName = model.Username,
            FullName = model.Name,
            Address = model.Address,
            Balance = model.Balance,
            Transactions = transactionsModel.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = transactionRepository.CountBySellerId(model.Id)
            }
        };
    }
}
