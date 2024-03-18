using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;

namespace WinterholdWeb.Controllers;

public class CategoryController : Controller
{   
    private readonly CategoryService service;

    public CategoryController(CategoryService service)
    {
        this.service = service;
    }

    [HttpGet("category")]
    public IActionResult Index()
    {
        return View();
    }
}
