using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasiliskWeb.ViewModels.Account;

public class RegisterVM
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage ="Password does not match")]
    public string ConfirmPassword { get; set; } = null!;
    public string Role { get; set; } = null!;
    public List<SelectListItem> Roles { get; set; } = null!;
}
