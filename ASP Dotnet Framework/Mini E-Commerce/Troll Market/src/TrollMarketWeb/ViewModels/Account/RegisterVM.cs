using System.ComponentModel.DataAnnotations;
using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels.Account;

public class RegisterVM
{
    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    [UniqueAccountUsername]
    public string Username { get; set; }

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    public string Password { get; set; }

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    [Display(Name ="Confirm Password")]
    [Compare("Password", ErrorMessage ="Password tidak cocok")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    public string Name { get; set; }

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    public string Address { get; set; }
}
