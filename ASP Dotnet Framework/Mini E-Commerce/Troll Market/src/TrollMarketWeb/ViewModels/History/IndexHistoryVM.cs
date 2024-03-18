using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketWeb.ViewModels.History;

public class IndexHistoryVM
{
    public List<HistoryTransactionVM>? Histories { get; set; }
    public PaginationVM? Pagination { get; set; }
    public List<SelectListItem>? Sellers { get; set; }
    public List<SelectListItem>? Buyers { get; set; }
    public long SellerId { get; set; }
    public long BuyerId { get; set; }
}
