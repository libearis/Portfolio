using System.ComponentModel.DataAnnotations;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels.Category;

namespace WinterholdWeb.Validation;

public class UniqueCategoryName : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null)
        {
            var dbContext = (WinterholdContext?)validationContext.GetService(typeof(WinterholdContext))?? throw new NullReferenceException("System Error");
            var isCategoryNameExist = dbContext.Categories.Any(cat=>cat.Name == value.ToString());
            if(isCategoryNameExist)
            {
                return new ValidationResult($"Nama {value} sudah ada, silahkan ganti nama lain");
            }
        }
        return ValidationResult.Success;
    }
}
