using BasiliskWeb.Services;
using BasiliskWeb.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

[Authorize(Roles ="Finance")]
public class CategoryController : Controller
{
    private readonly CategoryService service;
    public CategoryController(CategoryService categoryService)
    {
        service = categoryService;
    }
    public IActionResult Index(int pageNumber = 1, int pageSize = 5, string name ="")
    {
        var viewModel = service.Get(pageNumber, pageSize, name);
        return View(viewModel);
    }

    [HttpGet("category-insert")]
    public IActionResult InsertNewCategory()
    {
        return View("category-insert");
    }

    [HttpPost("category-insert")]
    public IActionResult InsertNewCategory(CategoryInsertVM viewModel)
    {
        if(!ModelState.IsValid)
        {
            return View("category-insert");
        }
        service.Insert(viewModel);
        return RedirectToAction("Index");
    }

    [HttpGet("updatecategory/{id}")]
    public IActionResult Update(long id)
    {
        var viewModel = service.GetById(id);
        return View("category-update", viewModel);
    }

    [HttpPost("updatecategory/{id}")]
    public IActionResult Update(CategoryUpdateVM viewModel)
    {
        if(!ModelState.IsValid)
        {
            return View("category-update", viewModel);
        }
        service.Update(viewModel);
        var indexModel = service.Get(1, 5, "");
        return View("Index", indexModel);
    }

    [HttpGet("harddelete/{id}")]
    public IActionResult HardDelete(long id)
    {
        var viewModel = service.Get(1, 5, "");
        try
        {
            service.HardDelete(id);
            return View("Index", viewModel);
        }
        catch (System.Exception)
        {
            return View("category-faildelete", viewModel);
            throw;
        }
    }
}
