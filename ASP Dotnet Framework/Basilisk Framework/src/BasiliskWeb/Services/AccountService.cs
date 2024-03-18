using System.Security.Claims;
using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Services;

public class AccountService
{
    private readonly IAccountRepository repository;

    public AccountService(IAccountRepository repository)
    {
        this.repository = repository;
    }

    private List<SelectListItem> GetRoles()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "Administrator",
                Value = "Administrator"
            },
            new SelectListItem()
            {
                Text = "Finance",
                Value = "Finance"
            },
            new SelectListItem()
            {
                Text = "Salesman",
                Value = "Salesman"
            }
        };
    }

    public RegisterVM GetRegisterModel()
    {
        return new RegisterVM()
        {
            Roles = GetRoles()
        };
    }

    public void InsertRegister(RegisterVM viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = viewModel.Role
        };

        repository.Insert(model);
    }

    private ClaimsPrincipal GetPrincipal(LoginVM viewModel)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, viewModel.Username),
            new Claim(ClaimTypes.Role, viewModel.Role)
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private AuthenticationTicket GetAuthenticationTicket(ClaimsPrincipal claimsPrincipal)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        {
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };

        return new AuthenticationTicket(claimsPrincipal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);
    }
    public AuthenticationTicket CheckLogin(LoginVM viewModel)
    {
        var model = repository.GetAccount(viewModel.Username);
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(viewModel.Password, model.Password);
        if(!isCorrectPassword) throw new Exception("Username or password is incorrect");

        viewModel = new LoginVM()
        {
            Username = model.Username,
            Password = model.Password,
            Role = model.Role
        };
        ClaimsPrincipal claimsPrincipal = GetPrincipal(viewModel);
        return GetAuthenticationTicket(claimsPrincipal);
    }

    public void ChangePassword(ChangePasswordVM viewModel)
    {
        var model = repository.GetAccount(viewModel.Username);
        bool isCorrectOldPassword = BCrypt.Net.BCrypt.Verify(viewModel.OldPassword, model.Password);
        if(!isCorrectOldPassword) throw new Exception("Old Password is Incorrect");
        else
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(viewModel.NewPassword);
        }

        repository.Update(model);
    }
}
