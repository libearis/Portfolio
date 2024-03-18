using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Product;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskWeb.Services;

public class ProductService
{
    private readonly IProductRepository repository;

    public ProductService(IProductRepository productRepository)
    {
        repository = productRepository;
    }

    private Category GetCategory(long id)
    {
        return repository.GetCategory(id);
    }
    
    private Supplier GetSupplier(long id)
    {
        return repository.GetSupplier(id);
    }

    private List<ProductCategoryVM> GetProductCategories()
    {
        List<ProductCategoryVM> productCategories = new List<ProductCategoryVM>();
        foreach(var category in repository.GetListCategory())
        {
            ProductCategoryVM productCategory = new ProductCategoryVM();
            productCategory.Id = category.Id;
            productCategory.CategoryName = category.Name;
            productCategories.Add(productCategory);
        }

        return productCategories;
    }

    private List<ProductSupplierVM> GetProductSuppliers()
    {
        List<ProductSupplierVM> productSuppliers = new List<ProductSupplierVM>();
        foreach(var supplier in repository.GetListSupplier())
        {
            ProductSupplierVM productSupplier = new ProductSupplierVM();
            productSupplier.Id = supplier.Id;
            productSupplier.SupplierName = supplier.CompanyName;
            productSuppliers.Add(productSupplier);
        }

        return productSuppliers;
    }

    public IndexViewModel GetProducts(long categoryId, long supplierId, int pageNumber, int pageSize, string productName)
    {
        var model = repository.Get( categoryId, supplierId, pageNumber, pageSize, productName).Select(pro => new ProductViewModel(){
            Id = pro.Id,
            ProductName = pro.Name,
            CategoryName = GetCategory(pro.CategoryId).Name,
            SupplierName = GetSupplier(pro.SupplierId??0).CompanyName??"No Supplier",
            Price = pro.Price
        });

        return new IndexViewModel()
        {
            Products = model.ToList(),
            Categories = GetProductCategories(),
            Suppliers = GetProductSuppliers(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(categoryId, supplierId, productName)
            },
            ProductName = productName,
            CategoryId = categoryId,
            SupplierId = supplierId
        };
    }

    public ProductInsertVM GetCategoriesAndSuppliers()
    {
        return new ProductInsertVM(){
            Categories = GetProductCategories(),
            Suppliers = GetProductSuppliers()
        };
    }

    public ProductUpdateVM GetById(long id)
    {
        var model = repository.GetById(id);

        return new ProductUpdateVM()
        {
            ProductName = model.Name,
            Categories = GetProductCategories(),
            Suppliers = GetProductSuppliers(),
            CategoryId = model.CategoryId,
            SupplierId = model.SupplierId,
            Price = model.Price,
            Stock = model.Stock,
            OnOrder = model.OnOrder,
            Description = model.Description
        };
    }

    public void Insert(ProductInsertVM productInsertVM)
    {
        var model = new Product()
        {
            Name = productInsertVM.ProductName,
            CategoryId = productInsertVM.CategoryId,
            SupplierId = productInsertVM.SupplierId,
            Price = productInsertVM.Price,
            Stock = productInsertVM.Stock,
            OnOrder = productInsertVM.OnOrder,
            Description = productInsertVM.Description
        };
        repository.Insert(model);
    }

    public void Update(ProductUpdateVM productUpdateVM)
    {
        var model = repository.GetById(productUpdateVM.Id);
        model.Name = productUpdateVM.ProductName;
        model.SupplierId = productUpdateVM.SupplierId;
        model.Price = productUpdateVM.Price;
        model.Stock = productUpdateVM.Stock;
        model.OnOrder = productUpdateVM.OnOrder;
        model.Description = productUpdateVM.Description;

        repository.Update(model);
    }

    public void SoftDelete(long id)
    {
        var model = repository.GetById(id);
        model.Discontinue = true;
        repository.Update(model);
    }
}
