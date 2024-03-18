using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WinterholdWeb.ViewModels.Book;

public class UpdateBookVM
{
    public string Code { get; set; } = null!;

    [Required(ErrorMessage ="{0} cannot be empty")]
    public string Title { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public List<SelectListItem> Authors { get; set; }

    [Required(ErrorMessage ="{0} cannot be empty")]
    public long AuthorId { get; set; }
    public bool IsBorrowed { get; set; }
    public string? Summary { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? TotalPage { get; set; }
}
