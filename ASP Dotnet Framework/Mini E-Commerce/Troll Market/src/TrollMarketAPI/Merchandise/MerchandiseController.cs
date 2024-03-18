using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketAPI.Services;

namespace TrollMarketAPI.Merchandise;

[Route("api/v1/merch")]
[ApiController]
[Authorize(Roles ="Seller")]
public class MerchandiseController : ControllerBase
{
    private readonly MerchandiseService service;

    public MerchandiseController(MerchandiseService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(string username, int pageNumber = 1)
    {
        var dto = service.GetDTO(username, pageNumber);
        return Ok(dto);
    }
}
