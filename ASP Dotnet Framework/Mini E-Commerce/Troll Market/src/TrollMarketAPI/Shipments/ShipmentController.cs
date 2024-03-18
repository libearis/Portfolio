using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketAPI.Services;

namespace TrollMarketAPI.Shipments;

[Route("api/v1/shipment")]
[ApiController]
[Authorize(Roles ="Admin")]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentService service;

    public ShipmentController(ShipmentService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Index(int pageNumber = 1)
    {
        var dto = service.GetIndexShipmentDTO(pageNumber);
        return Ok(dto);
    }

    [HttpPost("insert")]
    public IActionResult Insert(ShipmentDTO shipmentDTO)
    {
        service.InsertNew(shipmentDTO);
        return Ok(shipmentDTO);
    }

    [HttpPost("edit")]
    public IActionResult Edit(ShipmentDTO shipmentDTO)
    {
        service.Edit(shipmentDTO);
        return Ok(shipmentDTO);
    }

    [HttpPost("stop={id}")]
    public IActionResult StopService(long id)
    {
        service.StopService(id);
        return Ok(id);
    }

    [HttpPost("delete={id}")]
    public IActionResult Delete(long id)
    {
        service.Delete(id);
        return Ok(id);
    }
}
