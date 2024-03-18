using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Account;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    private readonly AdminService service;

    public AdminController(AdminService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("insert")]
    public IActionResult Insert(AdminRegisterVM viewModel)
    {
        if(ModelState.IsValid)
        {
            service.Insert(viewModel);
            return RedirectToAction("index");
        }
        return View("index");
    }
}
