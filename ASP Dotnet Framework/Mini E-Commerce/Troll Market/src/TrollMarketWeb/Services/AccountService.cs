using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Account;

namespace TrollMarketWeb.Services;

public class AccountService
{
    private readonly IAccountRepository repository;

    public AccountService(IAccountRepository repository)
    {
        this.repository = repository;
    }

    public void InsertBuyer(RegisterVM viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = "Buyer"
        };

        repository.Insert(model);
    }

    public void InsertSeller(RegisterVM viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = "Seller"
        };

        repository.Insert(model);
    }

    public void InsertAdmin(RegisterVM viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = "Admin"
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
        bool isCorrectRole = viewModel.Role == model.Role;
        if(!isCorrectPassword) throw new Exception("Password Salah");
        if(!isCorrectRole) throw new Exception("Role tidak sesuai");

        viewModel = new LoginVM()
        {
            Username = model.Username,
            Password = model.Password,
            Role = model.Role
        };
        ClaimsPrincipal claimsPrincipal = GetPrincipal(viewModel);
        return GetAuthenticationTicket(claimsPrincipal);
    }
}
