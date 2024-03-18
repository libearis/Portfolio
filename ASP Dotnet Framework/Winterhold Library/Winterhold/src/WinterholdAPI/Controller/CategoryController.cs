using Microsoft.AspNetCore.Mvc;
using WinterholdAPI.Category;
using WinterholdAPI.Services;

namespace WinterholdAPI.Controller;

[Route("api/v1/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService service;

    public CategoryController(CategoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult Get(string? cityName, int pageNumber = 1, int pageSize = 5)
    {
        var model = service.GetAllCategories(pageNumber, pageSize, cityName);
        return Ok(model);
    }

    [HttpPost("Insert")]
    public IActionResult Insert(CategoryDTO viewModel)
    {
        service.InsertNew(viewModel);
        return Ok(viewModel);
    }

    [HttpPut("Update")]
    public IActionResult Update(CategoryDTO viewModel)
    {
        service.Update(viewModel);
        return Ok();
    }

    [HttpGet("Delete-{categoryName}")]
    public IActionResult Delete(string categoryName)
    {
        service.Delete(categoryName);
        return Ok();
    }
}
