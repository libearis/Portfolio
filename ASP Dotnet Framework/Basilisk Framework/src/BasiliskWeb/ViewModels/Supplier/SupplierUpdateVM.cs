using System.ComponentModel.DataAnnotations;

namespace BasiliskWeb.ViewModels.Supplier;

public class SupplierUpdateVM
{
    public long Id { get; set; }
    [Required(ErrorMessage ="Company Name cannot empty")]
    public string CompanyName { get; set; } = null!;

    [Required(ErrorMessage ="Contact Person cannot empty")]
    public string? ContactPerson { get; set; }

    [Required(ErrorMessage ="Job Title cannot empty")]
    public string? JobTitle { get; set; }

    [Required(ErrorMessage ="Address cannot empty")]
    public string? Address { get; set; }

    [Required(ErrorMessage ="City cannot empty")]
    public string? City { get; set; }

    [Required(ErrorMessage ="Phone Number cannot empty")]
    public string? Phone { get; set; }

    [Required(ErrorMessage ="Email cannot empty")]
    public string? Email { get; set; }
}
