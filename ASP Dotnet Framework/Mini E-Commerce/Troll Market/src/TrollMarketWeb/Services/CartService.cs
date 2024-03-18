using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Cart;

namespace TrollMarketWeb.Services;

public class CartService
{
    private readonly ICartRepository repository;
    private readonly IBuyerRepository buyerRepository;
    private readonly ISellerRepository sellerRepository;
    private readonly ITransactionRepository transactionRepository;

    public CartService(ICartRepository repository, IBuyerRepository buyerRepository, ITransactionRepository transactionRepository, ISellerRepository sellerRepository)
    {
        this.repository = repository;
        this.buyerRepository = buyerRepository;
        this.transactionRepository = transactionRepository;
        this.sellerRepository = sellerRepository;
    }

    public IndexCartVM GetIndexCartVM(string username, int pageNumber)
    {
        var buyerModel = buyerRepository.GetByUsername(username);
        var model = repository.GetAll(buyerModel.Id, pageNumber, Constant.PageSize).Select(cart => new CartVM()
        {
            Id = cart.Id,
            BuyerId = cart.BuyerId,
            ProductName = cart.Product.Name,
            ShipmentName = cart.Shipment.Name,
            SellerName = cart.Seller.Name,
            Quantity = cart.Quantity,
            TotalPrice = Convert.ToInt64(cart.Product.UnitPrice * cart.Quantity + cart.Shipment.Price)
        });

        return new IndexCartVM()
        {
            Carts = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = repository.Count(buyerModel.Id)
            },
            BuyerId = buyerModel.Id
        };
    }

    public void Delete(long id)
    {
        var model = repository.GetById(id);
        repository.Delete(model);
    }

    public TransactionVM GetTransactionVM(long buyerId)
    {
        decimal totalPrice = 0;
        var buyerModel = buyerRepository.GetById(buyerId);
        var model = repository.GetAllPrice(buyerModel.Id).Select(cart => new CartVM()
        {
            TotalPrice = Convert.ToInt64(cart.Product.UnitPrice * cart.Quantity + cart.Shipment.Price)
        });

        foreach(var price in model)
        {
            totalPrice += price.TotalPrice;
        }

        if(buyerModel.Balance < totalPrice)throw new Exception("Saldo tidak mencukupi");

        return new TransactionVM()
        {
            BuyerId = buyerId,
            TotalPrice = totalPrice,
            CurrentBalance = buyerModel.Balance,
            RemainingBalance = buyerModel.Balance - totalPrice
        };
    }

    public void SetTransaction(long buyerId, decimal remainingBalance)
    {
        var carts = repository.GetAllPrice(buyerId);
        var buyerModel = buyerRepository.GetById(buyerId);

        foreach(var cart in carts)
        {
            var model = new Transaction()
            {
                SellerId = cart.SellerId,
                BuyerId = cart.BuyerId,
                ProductId = cart.ProductId,
                ShipmentId = cart.ShipmentId,
                Date = DateTime.Now,
                Quantity = cart.Quantity
            };
            var sellerModel = sellerRepository.GetById(cart.SellerId);
            sellerModel.Balance += Convert.ToInt32(cart.Product.UnitPrice) * cart.Quantity;

            transactionRepository.Insert(model);
            repository.Delete(cart);
        }
        buyerModel.Balance = Convert.ToInt32(remainingBalance);
        buyerRepository.Update(buyerModel);
    }
}
