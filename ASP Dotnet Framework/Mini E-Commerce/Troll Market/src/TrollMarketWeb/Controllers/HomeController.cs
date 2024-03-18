using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
