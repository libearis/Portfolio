using System.ComponentModel.DataAnnotations;
using WinterholdWeb.Validation;

namespace WinterholdWeb.ViewModels.Category;

public class InsertCategoryVM
{
    [Required(ErrorMessage ="{0} cannot be empty")]
    [UniqueCategoryName]
    public string CategoryName { get; set; }
    public int Floor { get; set; }
    public string Isle { get; set; }
    public string Bay { get; set; }
}
