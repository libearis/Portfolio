using Microsoft.AspNetCore.Mvc;
using WinterholdAPI.Services;

namespace WinterholdAPI.Controller;

[Route("api/v1/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService service;

    public CustomerController(CustomerService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var model = service.GetCustomers();
        return Ok(model);
    }

    [HttpGet("{memberNumber}")]
    public IActionResult Get(string memberNumber)
    {
        var model = service.GetCustomer(memberNumber);
        return Ok(model);
    }
}
