using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Cart;


namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Buyer")]
public class CartController : Controller
{
    private readonly CartService service;

    public CartController(CartService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = service.GetIndexCartVM(User.FindFirstValue(ClaimTypes.Name), pageNumber);
        return View(viewModel);
    }

    [HttpGet("delete-{id}")]
    public IActionResult Delete(long id)
    {
        service.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet("purchase-all-{buyerId}")]
    public IActionResult PurchaseAll(long buyerId)
    {
        try
        {
            var viewModel = service.GetTransactionVM(buyerId);
            return View("purchase-all", viewModel);
        }
        catch (System.Exception e)
        {
            ViewBag.Message = e.Message;
            var viewModel = service.GetIndexCartVM(User.FindFirstValue(ClaimTypes.Name), 1);
            return View("Index", viewModel);
        }
    }

    [HttpPost("purchase-all-{buyerId}")]
    public IActionResult PurchaseAll(long buyerId, decimal remainingBalance)
    {
        service.SetTransaction(buyerId, remainingBalance);
        return RedirectToAction("Index");
    }
}
