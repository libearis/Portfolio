using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly ProductService service;

    public ProductController(ProductService productService)
    {
        service = productService;
    }

    public IActionResult Index(long categoryId = 0, long supplierId = 0, int pageNumber = 1, int pageSize = 6, string productName = "")
    {
        var viewModel = service.GetProducts(categoryId, supplierId, pageNumber, pageSize, productName);
        return View("Index", viewModel);
    }

    [HttpGet("product-insert")]
    public IActionResult Insert()
    {
        var viewModel = service.GetCategoriesAndSuppliers();
        return View("product-insert", viewModel);
    }

    [HttpPost("product-insert")]
    public IActionResult Insert(ProductInsertVM viewModel)
    {
        service.Insert(viewModel);
        return RedirectToAction("Index");
    }

    [HttpGet("product-update/{id}")]
    public IActionResult Update(long id)
    {
        var viewModel = service.GetById(id);
        return View("product-update", viewModel);
    }

    [HttpPost("product-update/{id}")]
    public IActionResult Update(ProductUpdateVM viewModel)
    {
        service.Update(viewModel);
        var indexModel = service.GetProducts(0, 0 , 1, 6, "");
        return RedirectToAction("Index", indexModel);
    }

    public IActionResult SoftDelete(long id)
    {
        service.SoftDelete(id);
        var viewModel = service.GetProducts(0, 0 , 1, 6, "");
        return RedirectToAction("Index", viewModel);
    }
}
