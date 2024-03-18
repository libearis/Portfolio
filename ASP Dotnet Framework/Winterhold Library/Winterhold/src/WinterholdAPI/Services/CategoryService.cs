using WinterholdAPI.Category;
using WinterholdBusiness.Interfaces;

namespace WinterholdAPI.Services;

public class CategoryService
{
    private readonly ICategoryRepository repository;
    private readonly IBookRepository bookRepository;

    public CategoryService(ICategoryRepository repository, IBookRepository _bookRepository)
    {
        this.repository = repository;
        bookRepository = _bookRepository;
    }

    public IndexCategoryDTO GetAllCategories(int pageNumber, int pageSize, string name)
    {
        var model = repository.GetCategories(pageNumber, pageSize, name).Select(cat => new CategoryDTO()
        {
            Name = cat.Name,
            Floor = cat.Floor,
            Isle = cat.Isle,
            Bay = cat.Bay,
            TotalBook = bookRepository.CountTotalBookByCategory(cat.Name)
        });

        return new IndexCategoryDTO()
        {
            Categories = model.ToList(),
            Pagination = new PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountCategories(name)
            },
            Name = name
        };
    }

    public void InsertNew(CategoryDTO viewModel)
    {
        var model = new WinterholdDataAccess.Models.Category()
        {
            Name = viewModel.Name,
            Floor = viewModel.Floor,
            Isle = viewModel.Isle,
            Bay = viewModel.Bay
        };

        repository.Insert(model);
    }

    public void Update(CategoryDTO viewModel)
    {
        var model = repository.GetCategoryByName(viewModel.Name);
        model.Floor = viewModel.Floor;
        model.Isle = viewModel.Isle;
        model.Bay = viewModel.Bay;

        repository.Update(model);
    }

    public void Delete(string categoryName)
    {
        repository.Delete(repository.GetCategoryByName(categoryName));
    }
}
