using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize]
public class CustomerController : Controller
{
    private readonly CustomerService service;
    public CustomerController(CustomerService customerService)
    {
        service = customerService;
    }
    public IActionResult Index(int pageNumber = 1, int pageSize = 8, string companyName = "", string contactPerson = "")
    {
        var viewModel = service.Get(pageNumber, pageSize, companyName, contactPerson);
        return View(viewModel);
    }

    [HttpGet("customer-insert")]
    public IActionResult Insert()
    {
        return View("customer-insert");
    }

    [HttpPost("customer-insert")]
    public IActionResult Insert(CustomerInsertVM customerInsertVM)
    {
        if(!ModelState.IsValid)
        {
            return View("customer-insert");
        }
        service.Insert(customerInsertVM);
        return RedirectToAction("Index");
    }

    [HttpGet("customer-update/{id}")]
    public IActionResult Update(long id)
    {
        var viewModel = service.GetById(id);
        return View("customer-update", viewModel);
    }

    [HttpPost("customer-update/{id}")]
    public IActionResult Update(CustomerUpdateVM viewModel)
    {
        service.Update(viewModel);
        var indexModel = service.Get(1, 8, "", "");
        return RedirectToAction("Index", indexModel);
    }

    [HttpGet("customer-delete")]
    public IActionResult SoftDelete(long id)
    {
        service.SoftDelete(id);
        return RedirectToAction("Index");
    }
}
