namespace TrollMarketWeb.ViewModels.Seller;

public class SellerVM
{
    public long Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
    public string? Address { get; set; }
    public decimal Balance { get; set; }

    public List<SellerTransactionVM>? Transactions { get; set; }
    public PaginationVM? Pagination { get; set; }
}
