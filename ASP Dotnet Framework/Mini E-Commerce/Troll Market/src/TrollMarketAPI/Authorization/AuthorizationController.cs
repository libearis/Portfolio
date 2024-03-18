using Microsoft.AspNetCore.Mvc;
using TrollMarketAPI.Authorization;
using TrollMarketAPI.Services;

namespace TrollMarketAPI;

[Route("api/v1/Auth")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly AuthorizationService service;

    public AuthorizationController(AuthorizationService authorizationService)
    {
        service = authorizationService;
    }

    [HttpPost]
    public IActionResult Login(AuthRequestDTO requestDTO)
    {
        try
        {
            return Ok(service.GetToken(requestDTO));
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
}
