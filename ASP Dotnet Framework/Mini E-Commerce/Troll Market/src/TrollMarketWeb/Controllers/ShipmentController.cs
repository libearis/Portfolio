using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Admin")]
public class ShipmentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
