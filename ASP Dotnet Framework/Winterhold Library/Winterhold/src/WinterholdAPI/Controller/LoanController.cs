using Microsoft.AspNetCore.Mvc;
using WinterholdAPI.LoanDTOs;
using WinterholdAPI.Services;

namespace WinterholdAPI.Controller;

[Route("api/v1/loan")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly LoanService service;

    public LoanController(LoanService service)
    {
        this.service = service;
    }

    [HttpGet]
    public IActionResult GetAllLoans(int pageNumber = 1, int pageSize = 5, string? bookTitle="", string? customerName="", bool passedDueDate = false)
    {
        var model = service.GetAllLoans(pageNumber, pageSize, bookTitle, customerName, passedDueDate);
        return Ok(model);
    }

    [HttpGet("insertVM")]
    public IActionResult GetAllBooks()
    {
        var model = service.GetInsertVM();
        return Ok(model);
    }

    [HttpPost("insert-loan")]
    public IActionResult Insert(InsertLoanDTO viewModel)
    {
        service.InsertNew(viewModel);
        return Ok(viewModel);
    }

    [HttpGet("updateVM{id}")]
    public IActionResult GetUpdateVM(long id)
    {
        var model = service.GetUpdateVM(id);
        return Ok(model);
    }

    [HttpPut("update-loan")]
    public IActionResult Update(UpdateLoanDTO viewModel)
    {
        service.Update(viewModel);
        return Ok(viewModel);
    }

    [HttpPatch("return-{id}")]
    public IActionResult Return(long id)
    {
        service.Return(id);
        return Ok();
    }
}
