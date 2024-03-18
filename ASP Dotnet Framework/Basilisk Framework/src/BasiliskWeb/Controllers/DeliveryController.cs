using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Delivery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize]
public class DeliveryController : Controller
{
    private readonly DeliveryService service;
    public DeliveryController(DeliveryService deliveryService)
    {
        service = deliveryService;
    }
    public IActionResult Index(int pageNumber = 1, int pageSize = 5, string companyName = "")
    {
        var viewModel = service.Get(pageNumber, pageSize, companyName);
        return View(viewModel);
    }

    [HttpGet("delivery-insert")]
    public IActionResult Insert()
    {
        return View("delivery-insert");
    }

    [HttpPost("delivery-insert")]
    public IActionResult Insert(DeliveryInsertVM viewModel)
    {
        service.Insert(viewModel);
        return RedirectToAction("Index");
    }

    [HttpGet("delivery-update/{id}")]
    public IActionResult Update(long id)
    {
        var viewModel = service.GetById(id);
        return View("delivery-update", viewModel);
    }

    [HttpPost("delivery-update/{id}")]
    public IActionResult Update(DeliveryUpdateVM viewModel)
    {
        if(!ModelState.IsValid)
        {
            return View("delivery-update",viewModel);
        }
        service.Update(viewModel);
        var indexModel = service.Get(1, 5, "");
        return View("Index", indexModel);
    }

    [HttpGet("deliverydelete")]
    public IActionResult HardDelete(long id)
    {
        service.HardDelete(id);
        return RedirectToAction("Index");
    }
}
