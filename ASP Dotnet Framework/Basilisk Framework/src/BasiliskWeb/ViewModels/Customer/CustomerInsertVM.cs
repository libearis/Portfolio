using System.ComponentModel.DataAnnotations;

namespace BasiliskWeb.ViewModels.Customer;

public class CustomerInsertVM
{
    [Required(ErrorMessage ="Company Name cannot be empty")]
    public string? CompanyName { get; set; }

    [Required(ErrorMessage ="Contact Person cannot be empty")]
    public string? ContactPerson { get; set; }

    [Required(ErrorMessage ="Address cannot be empty")]
    public string? Address { get; set; }

    [Required(ErrorMessage ="City cannot be empty")]
    public string? City { get; set; }

    [Required(ErrorMessage ="Phone cannot be empty")]
    public string? Phone { get; set; }

    [Required(ErrorMessage ="Email cannot be empty")]
    public string? Email { get; set; }
}
