using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels.Category;
using BasiliskWeb.ViewModels;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Services;

public class CategoryService
{
    private readonly ICategoryRepository repository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        repository = categoryRepository;
    }

    public IndexViewModel Get(int pageNumber, int pageSize, string name)
    {
        var model = repository.Get(pageNumber, pageSize, name).Select(cat => new CategoryViewModel(){
            Id = cat.Id,
            CategoryName = cat.Name,
            CategoryDescription = cat.Description
        });

        return new IndexViewModel()
        {
            Categories = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(name)
            },
            Name = name
        };
    }

    public CategoryUpdateVM GetById(long id)
    {
        try
        {
            var model = repository.Get(id);
            return new CategoryUpdateVM()
            {
                Id = model.Id,
                CategoryName = model.Name,
                CategoryDescription = model.Description
            };
        }
        catch (System.Exception e)
        {
            throw new NullReferenceException(e.Message);
        }
    }

    public void Insert(CategoryInsertVM categoryInsertVM)
    {
        var model = new Category()
        {
          Name = categoryInsertVM.CategoryName,
          Description = categoryInsertVM.CategoryDescription     
        };

        repository.Insert(model);
    }

    public void Update(CategoryUpdateVM viewModel)
    {
        var model = repository.Get(viewModel.Id);
        model.Name = viewModel.CategoryName;
        model.Description = viewModel.CategoryDescription;

        repository.Update(model);
    }
    
    public void HardDelete(long id)
    {
        var model = repository.Get(id);
        repository.Delete(model);
    }
}
