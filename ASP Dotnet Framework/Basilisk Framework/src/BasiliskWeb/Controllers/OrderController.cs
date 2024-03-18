using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly OrderService service;

    public OrderController(OrderService orderService)
    {
        service = orderService;
    }

    [HttpGet("Index")]
    public IActionResult Index(DateTime orderDate, int pageNumber = 1, int pageSize = 5, string invoiceNumber = "", long customerId = 0, string salesEmployeeNumber = "", long deliveryId = 0)
    {
        var viewModel = service.Get(pageNumber, pageSize, invoiceNumber, customerId, salesEmployeeNumber, deliveryId, orderDate);
        return View("Index", viewModel);
    }

    [HttpGet("order-insert")]
    public IActionResult Insert()
    {
        var viewModel = service.GetListForInsert();
        return View("order-insert", viewModel);
    }

    [HttpPost("order-insert")]
    public IActionResult Insert(OrderInsertVM orderInsertVM)
    {
        service.Insert(orderInsertVM);
        return RedirectToAction("Index");
    }

    [HttpGet("order-update/{invoiceNumber}")]
    public IActionResult Update(string invoiceNumber)
    {
        var viewModel = service.GetByInvoiceNumber(invoiceNumber);
        return View("order-update", viewModel);
    }

    [HttpPost("order-update/{invoiceNumber}")]
    public IActionResult Update(OrderUpdateVM orderUpdateVM)
    {
        service.Update(orderUpdateVM);
        return RedirectToAction("Index");
    }

    [HttpGet("orderdetail/{invoiceNumber}")]
    public IActionResult OrderDetail(string invoiceNumber, string customerName, string salesEmployeeNumber, DateTime orderDate, int pageNumber = 1, int pageSize = 1)
    {
        var viewModel = service.GetDetailIndexVM(invoiceNumber, customerName, salesEmployeeNumber, orderDate, pageNumber, pageSize);
        return View("orderdetail", viewModel);
    }

    [HttpGet("orderdetail-insert/{invoiceNumber}")]
    public IActionResult InsertDetail(string invoiceNumber)
    {
        var viewModel = service.GetDetailInsert(invoiceNumber);
        return View("orderdetail-insert", viewModel);
    }

    [HttpPost("orderdetail-insert/{invoiceNumber}")]
    public IActionResult InsertDetail(OrderDetailInsertVM model)
    {
        service.InsertDetail(model);
        return RedirectToAction("Index");
    }

    [HttpGet("orderdetail-update/{id}")]
    public IActionResult UpdateDetail(long id)
    {
        var viewModel = service.GetDetailById(id);
        return View("orderdetail-update", viewModel);
    }

    [HttpPost("orderdetail-update/{id}")]
    public IActionResult UpdateDetail(OrderDetailUpdateVM model)
    {
        service.UpdateDetail(model);
        return RedirectToAction("Index");
    }

    [HttpGet("order-faildelete")]
    public IActionResult Delete(string invoiceNumber)
    {
        try
        {
            service.HardDelete(invoiceNumber);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("order-faildelete");
            throw;
        }
    }

    public IActionResult DeleteDetail(long id)
    {
        service.HardDeleteDetail(id);
        return RedirectToAction("Index");
    }
}
