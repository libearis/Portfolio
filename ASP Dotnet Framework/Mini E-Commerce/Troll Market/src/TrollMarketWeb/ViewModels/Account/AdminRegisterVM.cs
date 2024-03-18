using System.ComponentModel.DataAnnotations;
using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels.Account;

public class AdminRegisterVM
{
    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    [UniqueAccountUsername]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    [Display(Name ="Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;
}
