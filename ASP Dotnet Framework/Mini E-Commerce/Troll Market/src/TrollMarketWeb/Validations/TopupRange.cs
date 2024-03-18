using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.Validations;

public class TopupRange : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value != null)
        {
            if(Convert.ToInt32(value) <= 1000)
            {
                return new ValidationResult("Minimal top up adalah Rp10.000. Harap masukkan nominal yang sesuai");
            }
            else if(Convert.ToInt32(value) > 50000000)
            {
                return new ValidationResult("Maksimal top up adalah Rp50.000.000. Harap masukkan nominal yang sesuai");
            }
        }
        return ValidationResult.Success;
    }
}
