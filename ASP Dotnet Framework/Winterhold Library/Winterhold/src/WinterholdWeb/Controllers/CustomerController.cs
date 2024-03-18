using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;
using WinterholdWeb.ViewModels.Customer;

namespace WinterholdWeb.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerService service;

    public CustomerController(CustomerService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(int pageNumber = 1, int pageSize = 5, string memberNumber= "", string customerName= "", bool isExpired= false)
    {
        var viewModel = service.GetIndexVM(pageNumber, pageSize, memberNumber, customerName, isExpired);
        return View(viewModel);
    }

    [HttpGet("insert-customer")]
    public IActionResult Insert()
    {
        return View("insert-customer");
    }

    [HttpPost("insert-customer")]
    public IActionResult Insert(InsertCustomerVM viewModel)
    {
        if(ModelState.IsValid)
        {
            service.InsertNewCustomer(viewModel);
            return RedirectToAction("Index");
        }
        return View("insert-customer");
    }

    [HttpGet("update-customer{memberNumber}")]
    public IActionResult Update(string memberNumber)
    {
        var viewModel = service.GetUpdateVM(memberNumber);
        return View("update-customer", viewModel);
    }

    [HttpPost("update-customer{memberNumber}")]
    public IActionResult Update(UpdateCustomerVM viewModel)
    {
        service.Update(viewModel);
        return RedirectToAction("Index");
    }

    [HttpGet("delete-customer{memberNumber}")]
    public IActionResult Delete(string memberNumber)
    {
        var viewModel = service.GetUpdateVM(memberNumber);
        return View("delete-customer", viewModel);
    }

    [HttpPost("delete-customer{memberNumber}")]
    public IActionResult DeleteConfirm(string memberNumber)
    {
        try
        {
            service.Delete(memberNumber);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("fail-delete");
        }
    }

    [HttpGet("extend-{memberNumber}")]
    public IActionResult ExtendMembership(string memberNumber)
    {
        service.Extend(memberNumber);
        return RedirectToAction("Index");
    }

}
