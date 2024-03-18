using Microsoft.AspNetCore.Mvc;

namespace BasiliskWeb.Controllers;

public class RegionController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
