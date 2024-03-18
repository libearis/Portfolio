using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;
using WinterholdWeb.ViewModels.Book;

namespace WinterholdWeb.Controllers;

public class BookController : Controller
{
    private readonly BookService service;

    public BookController(BookService service)
    {
        this.service = service;
    }

    [HttpGet("Book/{categoryName}")]
    public IActionResult Index(string categoryName, int pageNumber = 1, int pageSize = 10, string title = "", string author = "", bool IsAvailable = true)
    {
        return View();
    }

    [HttpGet("Book/insert-{categoryName}")]
    public IActionResult Insert(string categoryName)
    {
        var viewModel = service.GetInsertModel(categoryName);
        return View("insert-book", viewModel);
    }

    [HttpPost("Book/insert-{categoryName}")]
    public IActionResult Insert(InsertBookVM viewModel)
    {
        if(ModelState.IsValid)
        {
            try
            {
                service.Insert(viewModel);
                return View("Index");
            }
            catch (System.Exception)
            {
                ViewBag.Message = "Book Code is already exist, please use another code";
                return View("insert-book", service.GetInsertModel(viewModel.CategoryName));
            }
        }

        return View("insert-book", service.GetInsertModel(viewModel.CategoryName));
    }

    [HttpGet("Book/update-{bookCode}")]
    public IActionResult Update(string bookCode)
    {
        var viewModel = service.GetUpdateModel(bookCode);
        return View("update-book", viewModel);
    }

    [HttpPost("Book/update-{bookCode}")]
    public IActionResult Update(UpdateBookVM viewModel)
    {
        service.Update(viewModel);
        return View("index");
    }

    [HttpGet("Book/delete-{bookCode}")]
    public IActionResult Delete(string bookCode)
    {
        var viewModel = service.GetUpdateModel(bookCode);
        return View("delete-book", viewModel);
    }

    [HttpPost("Book/delete-{bookCode}")]
    public IActionResult DeleteConfirm(string bookCode)
    {
        try
        {
            service.Delete(bookCode);
            return View("Index");
        }
        catch (System.Exception)
        {
            return View("fail-delete");
        }
    }
}
