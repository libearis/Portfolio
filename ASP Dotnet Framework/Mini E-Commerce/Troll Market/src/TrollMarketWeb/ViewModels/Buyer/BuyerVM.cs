using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels.Buyer;

public class BuyerVM
{
    public long Id { get; set; }
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
    public string? Address { get; set; }
    public decimal Balance { get; set; }

    [TopupRange]
    public decimal TopupValue { get; set; }
    public List<BuyerTransactionVM>? Transactions { get; set; }
    public PaginationVM? Pagination { get; set; }
}
