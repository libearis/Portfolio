using WinterholdBusiness.Interfaces;
using WinterholdWeb.ViewModels.Category;

namespace WinterholdWeb.Services;

public class CategoryService
{
    private readonly ICategoryRepository repository;
    private readonly IBookRepository bookRepository;

    public CategoryService(ICategoryRepository repository, IBookRepository _bookRepository)
    {
        this.repository = repository;
        bookRepository = _bookRepository;
    }

    public IndexCategoryVM GetAllCategories(int pageNumber, int pageSize, string name)
    {
        var model = repository.GetCategories(pageNumber, pageSize, name).Select(cat => new CategoryVM()
        {
            Name = cat.Name,
            Floor = cat.Floor,
            Isle = cat.Isle,
            Bay = cat.Bay,
            TotalBook = bookRepository.CountTotalBookByCategory(cat.Name)
        });

        return new IndexCategoryVM()
        {
            Categories = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountCategories(name)
            },
            Name = name
        };
    }
}
