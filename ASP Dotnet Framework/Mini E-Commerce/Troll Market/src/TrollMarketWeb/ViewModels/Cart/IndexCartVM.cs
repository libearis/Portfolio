namespace TrollMarketWeb.ViewModels.Cart;

public class IndexCartVM
{
    public List<CartVM> Carts { get; set; }
    public PaginationVM Pagination { get; set; }
    public long BuyerId { get; set; }
}
