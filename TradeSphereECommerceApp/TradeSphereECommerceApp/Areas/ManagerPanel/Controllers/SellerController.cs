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
    public class SellerController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/Seller
        public ActionResult Index()
        {
            return View(db.Sellers.Where(s => s.IsDeleted == false).ToList());
        }

        public ActionResult AllIndex()
        {
            return View(db.Sellers.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(seller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seller);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Seller");
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seller);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Seller");
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(seller);
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Seller");
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            seller.IsDeleted = false;
            seller.IsActive = true;
            db.Entry(seller).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Sellers.Find(id);
            seller.IsDeleted = true;
            seller.IsActive = false;
            db.Entry(seller).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}