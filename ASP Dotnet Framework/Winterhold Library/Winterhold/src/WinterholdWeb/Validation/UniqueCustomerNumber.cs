using System.ComponentModel.DataAnnotations;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels.Customer;

namespace WinterholdWeb.Validation;

public class UniqueCustomerNumber : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null)
        {
            var memberNumber = ((InsertCustomerVM)validationContext.ObjectInstance).MembershipNumber;
            var dbContext = (WinterholdContext?)validationContext.GetService(typeof(WinterholdContext))?? throw new NullReferenceException("System Error");
            var isFirstNameExist = dbContext.Customers.Any(cus=>cus.MembershipNumber == value.ToString());
            if(isFirstNameExist)
            {
                return new ValidationResult($"Member number {value} sudah ada, silahkan ganti dengan member number lain");
            }
        }
        return ValidationResult.Success;
    }
}
