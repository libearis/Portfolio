using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Supplier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Controllers;

[Authorize]
public class SupplierController : Controller
{
    private readonly SupplierService service;
    public SupplierController(SupplierService supplierService)
    {
        service = supplierService;
    }
    public IActionResult Index(int pageNumber = 1, int pageSize = 5, string companyName = "", string contactName = "", string jobTitle = "")
    {
        var viewModel = service.Get(pageNumber, pageSize, companyName, contactName, jobTitle);
        return View(viewModel);
    }

    [HttpGet("supplier-insert")]
    public IActionResult Insert()
    {
        return View("supplier-insert");
    }

    [HttpPost("supplier-insert")]
    public IActionResult Insert(SupplierInsertVM supplierInsertVM)
    {
        if(!ModelState.IsValid)
        {
            return View("supplier-insert");
        }
        service.Insert(supplierInsertVM);
        return RedirectToAction("Index");
    }

    [HttpGet("updatesupplier/{id}")]
    public IActionResult Update(long id)
    {
        var viewModel = service.GetById(id);
        return View("supplier-update", viewModel);
    }

    [HttpPost("updatesupplier/{id}")]
    public IActionResult Update(SupplierUpdateVM viewModel)
    {
        service.Update(viewModel);
        var indexModel = service.Get(1, 5, "", "", "");
        return RedirectToAction("Index", indexModel);
    }

    [HttpGet("softdelete/{id}")]
    public IActionResult SoftDelete(long id)
    {
        var viewModel = service.Get(1, 5, "", "", "");
        service.SoftDelete(id);
        return View("Index", viewModel);
    }

}
