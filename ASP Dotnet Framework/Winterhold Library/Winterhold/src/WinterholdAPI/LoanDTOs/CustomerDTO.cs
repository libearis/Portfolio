namespace WinterholdAPI.LoanDTOs;

public class CustomerDTO
{
    public string MembershipNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime MembershipExpireDate { get; set; }
}
