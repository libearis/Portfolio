using TrollMarketAPI.Shop;
using TrollMarketBusiness.Interfaces;

namespace TrollMarketAPI.Services;

public class MerchandiseService
{
    private readonly IShopRepository repository;
    private readonly ISellerRepository sellerRepository;

    public MerchandiseService(IShopRepository repository, ISellerRepository sellerRepository)
    {
        this.repository = repository;
        this.sellerRepository = sellerRepository;
    }

    public IndexShopDTO GetDTO(string username, int pageNumber)
    {
        var sellerModel = sellerRepository.GetByUsername(username);
        var model = repository.GetAllForSeller(sellerModel.Id, pageNumber, Constant.PageSize).Select(pro => new ShopDTO()
        {
            Id = pro.Id,
            Name = pro.Name,
            CategoryName = pro.Category.Name,
            Description = pro.Description,
            UnitPrice = pro.UnitPrice,
            Discontinue = pro.Discontinue == true? "Yes" : "No"
        });

        return new IndexShopDTO()
        {
            ShopDTOs = model.ToList(),
            Pagination = new PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = repository.CountAllForSeller(sellerModel.Id)
            }
        };
    }
}
