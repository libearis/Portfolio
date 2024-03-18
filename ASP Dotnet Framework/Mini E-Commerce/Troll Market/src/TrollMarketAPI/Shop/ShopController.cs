using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketAPI.Services;

namespace TrollMarketAPI.Shop;

[Route("api/v1/shop")]
[ApiController]
[Authorize(Roles ="Buyer")]
public class ShopController : ControllerBase
{
    private readonly ShopService service;

    public ShopController(ShopService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(int pageNumber = 1, string? productName="", string? categoryName="", string? description="")
    {
        var dto = service.GetAllProduct(pageNumber, productName, categoryName, description);
        return Ok(dto);
    }

    [HttpPost("addToCart")]
    public IActionResult AddToCart(CartDTO cartDTO)
    {
        service.AddCart(cartDTO);
        return Ok();
    } 
}
