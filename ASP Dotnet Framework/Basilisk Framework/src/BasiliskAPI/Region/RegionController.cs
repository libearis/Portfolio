using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasiliskAPI.Region;

[Route("api/v1/regions")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly RegionService service;
    
    public RegionController(RegionService regionService)
    {
        service = regionService;
    }

    [HttpGet]
    public IActionResult Get(string? cityName, int pageNumber = 1, int pageSize = 5)
    {
        var model = service.GetAllRegion(pageNumber, pageSize, cityName);
        return Ok(model);
    }

    [HttpPost]
    public IActionResult Insert(RegionDTO regionDTO)
    {
        service.InsertNew(regionDTO);
        return Ok(regionDTO);
    }

    [HttpPut]
    public IActionResult Put(RegionDTO viewModel)
    {
        service.Update(viewModel);
        return Ok();
    }

    [HttpGet("sales-detail")]
    public IActionResult GetSalesmenDetail(string? employeeNumber, string? fullName, string? level, string? superiorName, long id, int pageNumber = 1, int pageSize = 5)
    {
        var model = service.GetSalesmenDetail(employeeNumber, fullName, level, superiorName, id, pageNumber, pageSize);
        return Ok(model);
    }

    [HttpGet("{id}")]
    public IActionResult Delete(long id)
    {
        service.Delete(id);
        return Ok();
    }
}
