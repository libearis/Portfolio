using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Buyer;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Buyer")]
public class BuyerController : Controller
{
    private readonly BuyerService service;

    public BuyerController(BuyerService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = service.GetBuyerVM(User.FindFirstValue(ClaimTypes.Name), pageNumber);
        return View(viewModel);
    }

    [HttpPost("topup-{id}")]
    public IActionResult TopUp(BuyerVM viewModel)
    {
        if(ModelState.IsValid)
        {
            service.TopupBalance(viewModel);
            return RedirectToAction("Index");
        }
        var vm = service.GetBuyerVM(User.FindFirstValue(ClaimTypes.Name), 1);
        return View("failed-topup", vm);
    }

    [HttpGet("topupview")]
    public IActionResult TopupPage()
    {
        var vm = service.GetBuyerVM(User.FindFirstValue(ClaimTypes.Name), 1);
        return View("failed-topup", vm);
    }
}
