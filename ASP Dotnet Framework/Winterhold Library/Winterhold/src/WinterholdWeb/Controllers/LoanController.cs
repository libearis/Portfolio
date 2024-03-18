using Microsoft.AspNetCore.Mvc;

namespace WinterholdWeb.Controllers;

public class LoanController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
