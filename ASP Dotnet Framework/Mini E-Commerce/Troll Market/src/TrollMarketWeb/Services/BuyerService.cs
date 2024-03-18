using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Account;
using TrollMarketWeb.ViewModels.Buyer;

namespace TrollMarketWeb.Services;

public class BuyerService
{
    private readonly IBuyerRepository repository;
    private readonly ITransactionRepository transactionRepository;

    public BuyerService(IBuyerRepository repository, ITransactionRepository transactionRepository)
    {
        this.repository = repository;
        this.transactionRepository = transactionRepository;
    }

    public void InsertNew(RegisterVM viewModel)
    {
        var model = new Buyer()
        {
            Username = viewModel.Username,
            Name = viewModel.Name,
            Address = viewModel.Address,
        };

        repository.Insert(model);
    }

    public BuyerVM GetBuyerVM(string username, int pageNumber)
    {
        var model = repository.GetByUsername(username);
        var transactionsModel = transactionRepository.GetByBuyerId(model.Id, pageNumber, Constant.PageSize).Select(tran => new BuyerTransactionVM()
        {
            Id = tran.Id,
            ProductName = tran.Product.Name,
            ShipmentName = tran.Shipment.Name,
            Date = tran.Date,
            Quantity = tran.Quantity,
            TotalPrice = tran.Product.UnitPrice * tran.Quantity + tran.Shipment.Price
        });

        return new BuyerVM()
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
                TotalSize = transactionRepository.CountByBuyerId(model.Id)
            }
        };
    }

    public void TopupBalance(BuyerVM viewModel)
    {
        var model = repository.GetById(viewModel.Id);
        model.Balance += Convert.ToInt32(viewModel.TopupValue);

        repository.Update(model);
    }
}
