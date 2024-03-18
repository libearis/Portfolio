using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface IAuthorRepository
{
    public Author GetById(long id);
    public List<Author> GetAllAuthor(int pageNumber, int pageSize, string name);
    public List<Author> GetAllAuthor();
    public int CountAllAuthor(string name);
    public void InsertNew(Author model);
    public void Update(Author model);
    public void Delete(Author model);

}
