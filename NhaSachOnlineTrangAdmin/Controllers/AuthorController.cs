using Microsoft.AspNetCore.Mvc;
using NhaSachOnlineTrangAdmin.Models.Domain;
using NhaSachOnlineTrangAdmin.Repositories.Abstract;

namespace NhaSachOnlineTrangAdmin.Controllers
{
  public class AuthorController : Controller
  {
    private readonly IAuthorService service;
    public AuthorController(IAuthorService service)
    {
      this.service = service;
    }

    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Add(Author model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var result = service.Add(model);
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
      var data = service.GetAll();
      return View(data);
    }

    public IActionResult Update(int id)
    {
      var record = service.FindById(id);
      return View(record);
    }

    [HttpPost]
    public IActionResult Update(Author model)
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

    public IActionResult Delete(int id) => RedirectToAction("GetAll", new { result = service.Delete(id) });
  }
}
