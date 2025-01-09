using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthorizationFilter]
    public class CategoryController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/Category
        public ActionResult Index()
        {
            return View(db.Categories.Where(b => b.IsDeleted == false).ToList());
        }
        public ActionResult AllIndex()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(category);
        }
        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            category.IsDeleted = false;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            category.IsDeleted = true;
            category.IsActive = false;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}