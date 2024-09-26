using Microsoft.AspNetCore.Mvc;
using NhaSachOnlineTrangAdmin.Models.Domain;
using NhaSachOnlineTrangAdmin.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NhaSachOnlineTrangAdmin.Controllers
{
  public class BookController : Controller
  {
    private readonly IAuthorService authorService;
    private readonly IBookService bookService;
    private readonly IGenreService genreService;
    private readonly IPublisherService publisherService;

    public BookController(
      IAuthorService authorService, 
      IBookService bookService, 
      IGenreService genreService, 
      IPublisherService publisherService)
    {
      this.authorService = authorService;
      this.bookService = bookService;
      this.genreService = genreService;
      this.publisherService = publisherService;
    }

    public IActionResult Add()
    {
      var model = new Book();
      model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
      model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
      model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();

      return View(model);
    }

    [HttpPost]
    public IActionResult Add(Book model)
    {
      model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorID }).ToList();
      model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
      model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var result = bookService.Add(model);
      if (result)
      {
        TempData["msg"] = "Thêm Thành Công";
        return RedirectToAction("Add");
      }
      TempData["msg"] = "Đã xảy ra lỗi. Vui lòng kiểm tra lại.";
      return View(model);
    }

    public IActionResult GetAll()
    {
      var data = bookService.GetAll();
      return View(data);
    }

    public IActionResult Update(int id)
    {
      var model = bookService.FindById(id);
      model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorID }).ToList();
      model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
      model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
      return View(model);
    }

    [HttpPost]
    public IActionResult Update(Book model)
    {
      model.AuthorList = authorService.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorID }).ToList();
      model.GenreList = genreService.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
      model.PublisherList = publisherService.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();

      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var result = bookService.Update(model);
      if (result)
      {
        return RedirectToAction("GetAll");
      }
      TempData["msg"] = "Lỗi phía server";

      return View(model);
    }

    public IActionResult Delete(int id) => RedirectToAction("GetAll", new { result = bookService.Delete(id) });
  }
}
