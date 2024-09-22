using Microsoft.AspNetCore.Mvc;
using NhaSachOnlineTrangAdmin.Models.Domain;
using NhaSachOnlineTrangAdmin.Repositories.Abstract;

namespace NhaSachOnlineTrangAdmin.Controllers
{
  public class GenreController : Controller
  {
    private readonly IGenreService service;
    public GenreController(IGenreService service)
    {
      this.service = service;
    }

    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Add(Genre model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var result = service.Add(model);
      if (result)
      {
        TempData["msg"] = "Thêm Thành Công";
        //return RedirectToAction(nameof(Add));
        return RedirectToAction("Add");
      }
      return View();
    }

    public IActionResult GetAll()
    {
      var data = service.GetAll();
      return View(data);
    }


    public IActionResult Update(int id)
    {
      var record = service.FindById(id);
      return View(record);
    }

    [HttpPost]
    public IActionResult Update(Genre model)
    {
      if (!ModelState.IsValid)
      {
        TempData["msg"] = "Dữ Liệu Nhập Vào Không Hợp Lệ";
        return View(model);
      }
      var result = service.Update(model);
      if (result)
      {
        return RedirectToAction("GetAll");
      }
      TempData["msg"] = "Đã xảy ra lỗi. Vui lòng kiểm tra lại.";
      return View(model);
    }
  }
}
