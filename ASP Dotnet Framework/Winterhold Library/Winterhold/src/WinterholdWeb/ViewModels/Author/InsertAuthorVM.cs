using System.ComponentModel.DataAnnotations;

namespace WinterholdWeb.ViewModels.Author;

public class InsertAuthorVM
{
    [Required(ErrorMessage ="{0} cannot be empty")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage ="{0} cannot be empty")]
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    [Required(ErrorMessage ="First Name cannot be empty")]
    public DateTime BirthDate { get; set; }
    public DateTime? DeceasedDate { get; set; }
    public string? Education { get; set; }
    public string? Summary { get; set; }
}
