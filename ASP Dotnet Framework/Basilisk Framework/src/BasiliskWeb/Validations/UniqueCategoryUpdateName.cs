using System.ComponentModel.DataAnnotations;
using BasiliskWeb.ViewModels.Category;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Validations;

public class UniqueCategoryUpdateName : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null)
        {
            var dbContext = (BasiliskTFContext?)validationContext.GetService(typeof(BasiliskTFContext))?? throw new NullReferenceException("System Error");
            var categoryId = ((CategoryUpdateVM)validationContext.ObjectInstance).Id;
            var isFirstNameExist = dbContext.Categories.Any(cat=>cat.Name == value.ToString() && cat.Id != categoryId);
            if(isFirstNameExist)
            {
                return new ValidationResult($"Category Name with {value.ToString()} is existed, please use other name");
            }
        }
        return ValidationResult.Success;
    }
}
