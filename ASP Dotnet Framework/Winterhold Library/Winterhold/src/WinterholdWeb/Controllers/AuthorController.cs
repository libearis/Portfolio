using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;
using WinterholdWeb.ViewModels.Author;

namespace WinterholdWeb.Controllers;

public class AuthorController : Controller
{
    private readonly AuthorService service;

    public AuthorController(AuthorService service)
    {
        this.service = service;
    }

    [HttpGet("author")]
    public IActionResult Index(int pageNumber = 1, int pageSize = 10, string name="")
    {
        var viewModel = service.GetAllAuthor(pageNumber, pageSize, name);
        return View(viewModel);
    }

    [HttpGet("insert-author")]
    public IActionResult Insert()
    {
        return View("insert-author");
    }

    [HttpPost("insert-author")]
    public IActionResult Insert(InsertAuthorVM viewModel)
    {
        if(ModelState.IsValid)
        {
            service.InsertNew(viewModel);
            return RedirectToAction("Index");
        }
        return View("insert-author");
    }

    [HttpGet("update-author-{id}")]
    public IActionResult Update(long id)
    {
        var viewModel = service.GetUpdateModel(id);
        return View("update-author", viewModel);
    }

    [HttpPost("update-author-{id}")]
    public IActionResult Update(UpdateAuthorVM viewModel)
    {
        if(ModelState.IsValid)
        {
            try
            {
                service.Update(viewModel);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ViewBag.Message = "Author ID is already exist, please use other ID";
                return View("update-author", viewModel);
            }  
        }
        return View("update-author", viewModel);
    }

    [HttpGet("book-detail{id}")]
    public IActionResult Books(long id)
    {
        var viewModel = service.GetBookDetails(id);
        return View("author-books", viewModel);
    }

    [HttpGet("delete-author-{id}")]
    public IActionResult Delete(long id)
    {
        var viewModel = service.GetUpdateModel(id);
        return View("delete-author", viewModel);
    }

    [HttpPost("delete-author-{id}")]
    public IActionResult DeleteConfirm(long id)
    {
        try
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            return View("fail-delete");
        }
    }
}
