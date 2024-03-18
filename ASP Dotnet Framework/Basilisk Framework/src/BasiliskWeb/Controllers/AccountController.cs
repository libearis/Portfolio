using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

public class AccountController : Controller
{
    private readonly AccountService service;

    public AccountController(AccountService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Register()
    {
        var viewModel = service.GetRegisterModel();
        return View("register", viewModel);
    }

    [HttpPost]
    public IActionResult Register(RegisterVM registerVM)
    {
        service.InsertRegister(registerVM);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View("login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM viewModel)
    {
        try
        {
            var ticket = service.CheckLogin(viewModel);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                    );
                return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception e)
        {
            return View("fail-login");
        }
    }

    [HttpGet]
    public IActionResult ChangePassword(string username)
    {
        var model = new ChangePasswordVM()
        {
            Username = username
        };
        return View("change-password", model);
    }

    [HttpPost]
    public IActionResult ChangePassword(ChangePasswordVM viewModel)
    {
        try
        {
            service.ChangePassword(viewModel);
            return RedirectToAction("Index", "Dashboard");

        }
        catch (System.Exception e)
        {
            ViewBag.Message = e.Message;
            return View("change-password", viewModel);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View("access-denied");
    }
}
