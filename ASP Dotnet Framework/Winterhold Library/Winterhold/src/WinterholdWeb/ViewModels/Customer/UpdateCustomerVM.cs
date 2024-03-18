namespace WinterholdWeb.ViewModels.Customer;

public class UpdateCustomerVM
{
    public string MembershipNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
}
