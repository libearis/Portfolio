using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;
using TrollMarketWeb.ViewModels.Shop;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Seller")]
public class MerchandiseController : Controller
{
    private readonly MerchandiseService service;

    public MerchandiseController(MerchandiseService service)
    {
        this.service = service;
    }

    public IActionResult Index(int pageNumber = 1)
    {
        var viewModel = service.GetMerchandiseVM(User.FindFirstValue(ClaimTypes.Name), pageNumber);
        return View(viewModel);
    }

    [HttpGet("insert-new")]
    public IActionResult Insert()
    {
        return View("insert-new");
    }

    [HttpPost("insert-new")]
    public IActionResult Insert(ProductVM viewModel)
    {
        service.Insert(viewModel, User.FindFirstValue(ClaimTypes.Name));
        return RedirectToAction("Index");
    }

    [HttpGet("edit-product-{id}")]
    public IActionResult Edit(long id)
    {
        var viewModel = service.GetEditVM(id);
        return View("edit-product", viewModel);
    }

    [HttpPost("edit-product-{id}")]
    public IActionResult Edit(ProductVM viewModel)
    {
        service.Edit(viewModel);
        return RedirectToAction("Index");
    }

    [HttpGet("deleteproduct-{id}")]
    public IActionResult Delete(long id)
    {
        var viewModel = service.GetEditVM(id);
        return View("delete-product", viewModel);
    }

    [HttpPost("deleteproduct-{id}")]
    public IActionResult ConfirmDelete(long id)
    {
        service.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet("discontinue-{id}")]
    public IActionResult Discontinue(long id)
    {
        service.Discontinue(id);
        return RedirectToAction("Index");
    }
}
