using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketWeb.ViewModels.Cart;

namespace TrollMarketWeb.ViewModels.Shop;

public class IndexShopVM
{
    public List<ProductVM>? Products { get; set; }
    public PaginationVM? Pagination { get; set; }
    public List<SelectListItem>? ShipmentOptions { get; set; }
    public string? ProductName { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
}
