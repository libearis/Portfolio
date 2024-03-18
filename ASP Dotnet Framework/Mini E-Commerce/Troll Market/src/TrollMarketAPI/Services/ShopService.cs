using TrollMarketAPI.Shop;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Services;

public class ShopService
{
    private readonly ICartRepository cartRepository;
    private readonly IShopRepository shopRepository;
    private readonly IBuyerRepository buyerRepository;

    public ShopService(ICartRepository cartRepository, IShopRepository shopRepository, IBuyerRepository buyerRepository)
    {
        this.cartRepository = cartRepository;
        this.shopRepository = shopRepository;
        this.buyerRepository = buyerRepository;
    }

    public IndexShopDTO GetAllProduct(int pageNumber, string productName, string categoryName, string description)
    {
        var model = shopRepository.GetAll(pageNumber, Constant.PageSize, productName, categoryName, description).Select(pro => new ShopDTO()
        {
            Id = pro.Id,
            CategoryName = pro.Category.Name,
            SellerName = pro.Seller.Name,
            Name = pro.Name,
            Description = pro.Description,
            UnitPrice = pro.UnitPrice
        }).ToList();

        return new IndexShopDTO()
        {
            ShopDTOs = model,
            Pagination = new PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = shopRepository.CountAll(productName, categoryName, description)
            }
        };
    }

    public void AddCart(CartDTO cartDTO)
    {
        var buyerModel = buyerRepository.GetByUsername(cartDTO.Username);
        Cart sameProductCart = cartRepository.CheckExistingProduct(cartDTO.ProductId, cartDTO.ShipmentId);
        if(sameProductCart != null)
        {
            sameProductCart.Quantity += cartDTO.Quantity;
            cartRepository.Update(sameProductCart);
        }
        else
        {
            var model = new Cart()
            {
                BuyerId = buyerModel.Id,
                SellerId = shopRepository.GetById(cartDTO.ProductId).SellerId,
                ProductId = cartDTO.ProductId,
                ShipmentId = cartDTO.ShipmentId,
                Quantity = cartDTO.Quantity
            };

            cartRepository.Insert(model);
        }
    }
}
