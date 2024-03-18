using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
