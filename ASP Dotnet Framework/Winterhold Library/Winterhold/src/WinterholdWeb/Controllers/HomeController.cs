using Microsoft.AspNetCore.Mvc;

namespace WinterholdWeb.Controllers;

public class HomeController : Controller
{
[HttpGet]
    public IActionResult Index()
    {
        return View("index");
    }
}
