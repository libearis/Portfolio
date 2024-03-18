using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Merchandise;
using TrollMarketWeb.ViewModels.Shop;

namespace TrollMarketWeb.Services;

public class MerchandiseService
{
    private readonly IShopRepository repository;
    private readonly ISellerRepository sellerRepository;

    public MerchandiseService(IShopRepository repository, ISellerRepository sellerRepository)
    {
        this.repository = repository;
        this.sellerRepository = sellerRepository;
    }

    public MerchandiseVM GetMerchandiseVM(string username, int pageNumber)
    {
        var seller = sellerRepository.GetByUsername(username);
        var model = repository.GetAllForSeller(seller.Id, pageNumber, Constant.PageSize).Select(pro => new ProductVM()
        {
            Id = pro.Id,
            CategoryName = pro.Category.Name,
            Name = pro.Name,
            Description = pro.Description,
            Discontinue = pro.Discontinue,
            IsPurchased = repository.IsProductPurchased(pro.Id)
        });

        return new MerchandiseVM()
        {
            Products = model.ToList(),
            Pagination = new ViewModels.PaginationVM()
            {
                PageNumber = pageNumber,
                PageSize = Constant.PageSize,
                TotalSize = repository.CountAllForSeller(seller.Id)
            }
        };
    }

    public void Insert(ProductVM viewModel, string username)
    {
        Seller sellerModel = sellerRepository.GetByUsername(username);
        Category categoryModel = repository.FindCategoryName(viewModel.CategoryName);
        if(categoryModel == null)
        {
            categoryModel = new Category()
            {
                Name = viewModel.CategoryName
            };
            repository.InsertCategory(categoryModel);

            categoryModel = repository.FindCategoryName(viewModel.CategoryName);
        }

        var model = new Product()
        {
            Name = viewModel.Name,
            CategoryId = categoryModel.Id,
            SellerId = sellerModel.Id,
            Description = viewModel.Description,
            UnitPrice = viewModel.UnitPrice,
            Discontinue = viewModel.Discontinue
        };

        repository.Insert(model);
    }

    public ProductVM GetEditVM(long id)
    {
        Product model = repository.GetById(id);

        return new ProductVM()
        {
            Id = model.Id,
            Name = model.Name,
            CategoryName = model.Category.Name,
            Description = model.Description,
            UnitPriceInt = Convert.ToInt32(model.UnitPrice),
            Discontinue = model.Discontinue
        };
    }

    public void Edit(ProductVM viewModel)
    {
        Category categoryModel = repository.FindCategoryName(viewModel.CategoryName);
        if(categoryModel == null)
        {
            categoryModel = new Category()
            {
                Name = viewModel.CategoryName
            };
            repository.InsertCategory(categoryModel);

            categoryModel = repository.FindCategoryName(viewModel.CategoryName);
        }

        var model = repository.GetById(viewModel.Id);
        model.Name = viewModel.Name;
        model.CategoryId = categoryModel.Id;
        model.Description = viewModel.Description;
        model.UnitPrice = viewModel.UnitPriceInt;
        model.Discontinue = viewModel.Discontinue;

        repository.Update(model);
    }

    public void Delete(long id)
    {
        var model = repository.GetById(id);
        repository.HardDelete(model);
    }

    public void Discontinue(long id)
    {
        var model = repository.GetById(id);
        model.Discontinue = true;
        repository.Update(model);
    }
}
