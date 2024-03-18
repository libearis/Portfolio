using System.Data.Common;
using System.Net;
using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly TrollMarketContext dbContext;

    public ShopRepository(TrollMarketContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Product> GetAll(int pageNumber, int pageSize, string productName, string categoryName, string description)
    {
        return dbContext.Products.Include("Category").Include("Seller")
        .Where(pro => pro.Name.Contains(productName??"") && pro.Discontinue == false &&
        pro.Category.Name.Contains(categoryName??"") && pro.Description.Contains(description??""))
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public List<Product> GetAllForSeller(long id, int pageNumber, int pageSize)
    {
        return dbContext.Products.Include("Category")
        .Where(pro => pro.SellerId == id)
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public int CountAll(string productName, string categoryName, string description)
    {
        return dbContext.Products.Include("Category")
        .Where(pro => pro.Name.Contains(productName??"") && pro.Discontinue == false &&
        pro.Category.Name.Contains(categoryName??"") && pro.Description.Contains(description??""))
        .Count();
    }

    public int CountAllForSeller(long id)
    {
        return dbContext.Products.Include("Category")
        .Where(pro => pro.SellerId == id)
        .Count();
    }

    public Product GetById(long id)
    {
        return dbContext.Products.Include("Seller").Include("Category").Where(pro => pro.Id == id).First();
    }

    public Category? FindCategoryName(string name)
    {
        var model = dbContext.Categories.Where(cat => cat.Name == name);
        if(model.Count() == 0)
        {
            return null;
        }

        return model.First();
    }

    public bool IsProductPurchased(long id)
    {
        return (dbContext.Transactions.Where(tran => tran.ProductId == id).Count() != 0? true : false)||
        (dbContext.Carts.Where(cart => cart.ProductId == id).Count() != 0? true : false)? true : false;
    }

    public void InsertCategory(Category model)
    {
        dbContext.Categories.Add(model);
        dbContext.SaveChanges();
    }
    
    public void Insert(Product model)
    {
        dbContext.Products.Add(model);
        dbContext.SaveChanges();
    }


    public void Update(Product model)
    {
        dbContext.Products.Update(model);
        dbContext.SaveChanges();
    }

    public void HardDelete(Product model)
    {
        dbContext.Products.Remove(model);
        dbContext.SaveChanges();
    }
}
