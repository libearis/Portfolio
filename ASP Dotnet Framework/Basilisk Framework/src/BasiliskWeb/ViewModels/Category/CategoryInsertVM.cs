using System.ComponentModel.DataAnnotations;
using BasiliskWeb.Validations;

namespace BasiliskWeb.ViewModels.Category;

public class CategoryInsertVM
{
    [Required(ErrorMessage ="Category Name cannot be empty")]
    [UniqueCategoryInsertName]
    public string CategoryName { get; set; }

    [Required(ErrorMessage ="Category Description cannot be empty")]
    public string CategoryDescription { get; set; }
}
