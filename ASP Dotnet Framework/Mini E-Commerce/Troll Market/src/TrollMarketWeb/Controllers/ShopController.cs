using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Buyer")]
public class ShopController : Controller
{
    private readonly ShopService service;

    public ShopController(ShopService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(int pageNumber = 1, string productName ="", string categoryName ="", string description ="")
    {
        var viewModel = service.GetIndexShopVM(pageNumber, productName, categoryName, description);
        return View(viewModel);
    }
}
