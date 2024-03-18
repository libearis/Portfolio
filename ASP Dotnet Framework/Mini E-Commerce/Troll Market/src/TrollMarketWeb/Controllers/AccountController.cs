using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Account;

namespace TrollMarketWeb.Controllers;

public class AccountController : Controller
{
    private readonly AccountService accountService;
    private readonly BuyerService buyerService;
    private readonly SellerService sellerService;

    public AccountController(AccountService accountService, BuyerService buyerService, SellerService sellerService)
    {
        this.accountService = accountService;
        this.buyerService = buyerService;
        this.sellerService = sellerService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM viewModel)
    {
        if(ModelState.IsValid)
        {
            try
            {
                var ticket = accountService.CheckLogin(viewModel);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ticket.Principal,
                        ticket.Properties
                        );

                    return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
        return View("login");
    }

    [HttpGet("register-buyer")]
    public IActionResult RegisterBuyer()
    {
        return View("register-buyer");
    }

    [HttpPost("register-buyer")]
    public IActionResult RegisterBuyer(RegisterVM viewModel)
    {
        if(ModelState.IsValid)
        {
            accountService.InsertBuyer(viewModel);
            buyerService.InsertNew(viewModel);

            return RedirectToAction("Index");
        }

        return View("register-buyer");
    }

    [HttpGet("register-seller")]
    public IActionResult RegisterSeller()
    {
        return View("register-seller");
    }

    [HttpPost("register-seller")]
    public IActionResult RegisterSeller(RegisterVM viewModel)
    {
        if(ModelState.IsValid)
        {
            accountService.InsertSeller(viewModel);
            sellerService.InsertNew(viewModel);

            return RedirectToAction("Index");
        }

        return View("register-seller");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }
    
    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet("Login")]
    public IActionResult FailedLogin()
    {
        return View("notLogin");
    }
}
