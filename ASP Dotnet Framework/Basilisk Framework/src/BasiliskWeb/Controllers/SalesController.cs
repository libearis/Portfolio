using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

public class SalesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
