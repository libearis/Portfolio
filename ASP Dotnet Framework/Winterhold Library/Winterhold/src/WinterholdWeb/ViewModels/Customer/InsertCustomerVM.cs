using System.ComponentModel.DataAnnotations;
using WinterholdWeb.Validation;
namespace WinterholdWeb.ViewModels.Customer;

public class InsertCustomerVM
{
    [Required(ErrorMessage ="{0} cannot be empty")]
    [UniqueCustomerNumber]
    public string MembershipNumber { get; set; } = null!;

    [Required(ErrorMessage ="{0} cannot be empty")]
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    [Required(ErrorMessage ="{0} cannot be empty")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage ="{0} cannot be empty")]
    public string Gender { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? IdentityNumber { get; set; }
    public DateTime MembershipExpireDate { get{
        return DateTime.Now.AddYears(2);
    }}
}
