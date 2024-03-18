using System.ComponentModel.DataAnnotations;

namespace BasiliskWeb.ViewModels.Account;

public class ChangePasswordVM
{
    public string Username { get; set; } = null!;
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;

    [Compare("NewPassword", ErrorMessage ="Password does not match")]
    public string ConfirmNewPassword { get; set; } = null!;
}
