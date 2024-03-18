using System.ComponentModel.DataAnnotations;
using TrollMarketDataAccess.Models;

namespace TrollMarketWeb.Validations;

public class UniqueAccountUsername : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null)
        {
            var dbContext = (TrollMarketContext?)validationContext.GetService(typeof(TrollMarketContext))?? throw new NullReferenceException("System Error");
            var findRole = dbContext.Accounts.Where(acc => acc.Username == value.ToString());
            var isUsernameExist = dbContext.Accounts.Any(acc=>acc.Username == value.ToString() && acc.Role == findRole.Select(acc => acc.Role).ToString());
            if(isUsernameExist)
            {
                return new ValidationResult($"Username {value} dengan Role ini sudah ada");
            }
        }
        return ValidationResult.Success;
    }
}
