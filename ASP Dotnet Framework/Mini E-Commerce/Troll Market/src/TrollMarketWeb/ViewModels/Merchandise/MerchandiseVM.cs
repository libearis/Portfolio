using TrollMarketWeb.ViewModels.Shop;

namespace TrollMarketWeb.ViewModels.Merchandise;

public class MerchandiseVM
{
    public List<ProductVM>? Products { get; set; }
    public PaginationVM? Pagination { get; set; }
}
