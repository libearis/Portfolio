using Microsoft.AspNetCore.Mvc;
using WinterholdAPI.Services;

namespace WinterholdAPI.Controller;

[Route("api/v1/Book")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookService service;

    public BookController(BookService service)
    {
        this.service = service;
    }

    [HttpGet("{categoryName}")]
    public IActionResult Get(string categoryName, int pageNumber = 1, int pageSize = 5, string? title="", string? author="", bool IsAvailable = false)
    {
        var model = service.GetIndexBookVM(categoryName, pageNumber, pageSize, title, author, IsAvailable);
        return Ok(model);
    }
}
