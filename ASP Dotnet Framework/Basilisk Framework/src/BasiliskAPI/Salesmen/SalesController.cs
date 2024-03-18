using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskAPI.Salesmen;

[Authorize]
[Route("api/v1/sales")]
[ApiController]
public class SalesController : ControllerBase
{
    private readonly SalesmenService service;

    public SalesController(SalesmenService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Get(int pageNumber = 1, int pageSize = 5)
    {
        var model = service.GetAllSales(pageNumber, pageSize);
        return Ok(model);
    }
}
