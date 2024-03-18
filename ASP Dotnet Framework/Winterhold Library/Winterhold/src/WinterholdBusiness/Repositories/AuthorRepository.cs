using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly WinterholdContext dbContext;

    public AuthorRepository(WinterholdContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Author GetById(long id)
    {
        return dbContext.Authors.Find(id)?? throw new NullReferenceException("Id Author tidak ada");
    }

    public List<Author> GetAllAuthor(int pageNumber, int pageSize, string name)
    {
        return dbContext.Authors.Where(aut => (aut.FirstName + " " + aut.LastName).Contains(name??""))
        .Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }

    public List<Author> GetAllAuthor()
    {
        return dbContext.Authors.ToList();
    }

    public int CountAllAuthor(string name)
    {
        return dbContext.Authors.Where(aut => (aut.FirstName + " " + aut.LastName).Contains(name??""))
        .Count();
    }

    public void InsertNew(Author model)
    {
        dbContext.Authors.Add(model);
        dbContext.SaveChanges();
    }

    public void Update(Author model)
    {
        dbContext.Authors.Update(model);
        dbContext.SaveChanges();
    }

    public void Delete(Author model)
    {
        dbContext.Authors.Remove(model);
        dbContext.SaveChanges();
    }
}
