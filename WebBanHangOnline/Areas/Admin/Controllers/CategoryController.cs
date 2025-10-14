using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
  public class CategoryController : Controller
  {
    ApplicationDbContext dbConnect = new ApplicationDbContext();
    // GET: Admin/Category
    public ActionResult Index()
    {
      var categories = dbConnect.Categories.ToList();
      return View(categories);
    }

    public ActionResult Add()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Add(Category model)
    {
      if (ModelState.IsValid)
      {
        model.CreatedDate = DateTime.Now;
        model.ModifiedDate = DateTime.Now;
        model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
        dbConnect.Categories.Add(model);
        dbConnect.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(model);
    }

    public ActionResult Edit(int id)
    {
      var item = dbConnect.Categories.Find(id);
      return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Category model)
    {
      if (ModelState.IsValid)
      {
        dbConnect.Categories.Attach(model);
        model.ModifiedDate = DateTime.Now;
        model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
        dbConnect.Entry(model).Property(e => e.Title).IsModified = true;
        dbConnect.Entry(model).Property(e => e.Description).IsModified = true;
        dbConnect.Entry(model).Property(e => e.Alias).IsModified = true;
        dbConnect.Entry(model).Property(e => e.SeoTitle).IsModified = true;
        dbConnect.Entry(model).Property(e => e.SeoDescription).IsModified = true;
        dbConnect.Entry(model).Property(e => e.SeoKeywords).IsModified = true;
        dbConnect.Entry(model).Property(e => e.Position).IsModified = true;
        dbConnect.Entry(model).Property(e => e.ModifiedDate).IsModified = true;
        dbConnect.Entry(model).Property(e => e.Modifiedby).IsModified = true;

        dbConnect.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(model);
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      var item = dbConnect.Categories.Find(id);
      if (item != null)
      {
        dbConnect.Categories.Remove(item);
        dbConnect.SaveChanges();
        return Json (new { success = true });
      }
      return Json(new { success = false });
    }
  }
}