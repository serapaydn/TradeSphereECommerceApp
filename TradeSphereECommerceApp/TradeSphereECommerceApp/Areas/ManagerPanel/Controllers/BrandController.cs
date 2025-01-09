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
    public class BrandController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/Brand
        public ActionResult Index()
        {
            Manager manager = (Manager)Session["manager"];
            var brands = db.Brand.Where(b => b.IsDeleted == false && b.Manager_ID == manager.ID).ToList();
            return View(brands);
        }
        public ActionResult AllIndex()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var brands = db.Brand.Where(p => p.Manager_ID == manager.ID).ToList();
            return View(brands);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                Manager manager = (Manager)Session["manager"];
                brand.Manager_ID = manager.ID;
                db.Brand.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            Manager manager = (Manager)Session["manager"];

            if (manager == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            Manager manager = (Manager)Session["manager"];

            if (manager == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (brand.Manager_ID != manager.ID)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            Manager manager = (Manager)Session["manager"];
            Brand brand = db.Brand.FirstOrDefault(b => b.ID == id && b.Manager_ID == manager.ID);

            if (brand == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            return View(brand);
        }
        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Brand");
            }

            Manager manager = (Manager)Session["manager"];
            Brand brand = db.Brand.FirstOrDefault(b => b.ID == id && b.Manager_ID == manager.ID);

            if (brand == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            brand.IsDeleted = false;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = (Manager)Session["manager"];
            Brand brand = db.Brand.FirstOrDefault(b => b.ID == id && b.Manager_ID == manager.ID);

            if (brand == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            brand.IsDeleted = true;
            brand.IsActive = false;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}