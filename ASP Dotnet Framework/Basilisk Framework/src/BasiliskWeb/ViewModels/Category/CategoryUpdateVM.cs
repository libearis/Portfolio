using System.ComponentModel.DataAnnotations;
using BasiliskWeb.Validations;

namespace BasiliskWeb.ViewModels.Category;

public class CategoryUpdateVM
{
    public long Id { get; set; }
    
    [Required(ErrorMessage ="Category Name cannot be empty")]
    [UniqueCategoryUpdateName]
    public string CategoryName { get; set; }

    [Required(ErrorMessage ="Category Description cannot be empty")]
    public string CategoryDescription { get; set; }
}
