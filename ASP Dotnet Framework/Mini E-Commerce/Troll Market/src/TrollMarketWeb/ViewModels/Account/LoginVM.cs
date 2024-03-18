using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketWeb.ViewModels.Account;

public class LoginVM
{
    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    public string Username { get; set; }

    [Required(ErrorMessage ="{0} tidak boleh kosong")]
    public string Password { get; set; }

    public string Role { get; set; }
}
