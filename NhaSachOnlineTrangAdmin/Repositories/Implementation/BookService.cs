using NhaSachOnlineTrangAdmin.Models.Domain;
using NhaSachOnlineTrangAdmin.Repositories.Abstract;

namespace NhaSachOnlineTrangAdmin.Repositories.Implementation
{
  public class BookService : IBookService
  {
    private readonly ApplicationDbContext context;

    public BookService(ApplicationDbContext context)
    {
      this.context = context;
    }
    // xử lý ngoại lệ (try catch)
    public bool Add(Book model)
    {
      try
      {
        context.Books.Add(model);
        context.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Delete(int id)
    {
      try
      {
        var data = this.FindById(id);
        if (data == null)
        {
          return false;
        }
        context.Books.Remove(data);
        context.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public Book FindById(int id)
    {
      return context.Books.Find(id);
    }

    public IEnumerable<Book> GetAll()
    {
      return context.Books.ToList();
    }

    public bool Update(Book model)
    {
      try
      {
        context.Books.Update(model);
        context.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
