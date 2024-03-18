using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Seller")]
public class SellerController : Controller
{
    private readonly SellerService service;

    public SellerController(SellerService service)
    {
        this.service = service;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = service.GetSellerVM(User.FindFirstValue(ClaimTypes.Name), pageNumber);
        return View(viewModel);
    }
}
