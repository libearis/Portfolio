using BasiliskBusiness.Interfaces;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly BasiliskTFContext dbContext;

    public ProductRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }
    public int Count()
    {
        return dbContext.Products.Count();
    }
    public int Count(long categoryId, long supplierId, string productName)
    {
        if(categoryId == 0 && supplierId == 0)
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"")).Count();
        }
        else if(categoryId != 0 && supplierId == 0)
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"") &&
            pro.CategoryId == categoryId).Count();
        }
        else if(categoryId == 0 && supplierId != 0)
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"") &&
            pro.SupplierId == supplierId).Count();
        }
        else
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"") &&
            pro.CategoryId == categoryId && pro.SupplierId == supplierId).Count();
        }
    }

    public List<Product> Get(int pageNumber, int pageSize)
    {
        return dbContext.Products.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }
    public List<Product> Get(long categoryId, long supplierId, int pageNumber, int pageSize, string productName)
    {
        if(categoryId == 0 && supplierId == 0)
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??""))
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
        else if(categoryId != 0 && supplierId == 0)
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"") &&
            pro.CategoryId == categoryId)
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
        else if(categoryId == 0 && supplierId != 0)
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"") &&
            pro.SupplierId == supplierId)
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
        else
        {
            return dbContext.Products.Where(pro=>pro.Name.Contains(productName??"") &&
            pro.CategoryId == categoryId && pro.SupplierId == supplierId)
            .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
        }
    }


    public Product GetById(long id)
    {
        return dbContext.Products.Find(id)?? throw new NullReferenceException($"Id {id} is not found");
    }

    public Category GetCategory(long id)
    {
        return dbContext.Categories.Find(id)?? throw new NullReferenceException($"Id {id} is not found");
    }

    public List<Category> GetListCategory()
    {
        return dbContext.Categories.ToList();
    }

    public List<Supplier> GetListSupplier()
    {
        return dbContext.Suppliers.ToList();
    }

    public Supplier GetSupplier(long id)
    {
        return dbContext.Suppliers.Find(id)?? throw new NullReferenceException($"Id {id} is not found");
    }

    public void Insert(Product product)
    {
        dbContext.Products.Add(product);
        dbContext.SaveChanges();
    }

    public void Update(Product product)
    {
        dbContext.Products.Update(product);
        dbContext.SaveChanges();
    }
}
