using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.Services;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles ="Admin")]
public class HistoryController : Controller
{
    private readonly HistoryService service;

    public HistoryController(HistoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(long sellerId, long buyerId, int pageNumber = 1)
    {
        var viewModel = service.GetIndexHistoryVM(pageNumber, sellerId, buyerId);
        return View(viewModel);
    }
}
