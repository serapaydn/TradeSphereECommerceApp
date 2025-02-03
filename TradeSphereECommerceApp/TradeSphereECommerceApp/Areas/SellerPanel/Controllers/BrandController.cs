using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    [SellerAuthorizationFilter]
    public class BrandController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/Brand
        public ActionResult Index()
        {
            Seller seller = (Seller)Session["seller"];
            var brands = db.Brand.Where(b => b.IsDeleted == false && b.Seller_ID == seller.ID).ToList();
            return View(brands);
        }

        public ActionResult AllIndex()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }
            var brands = db.Brand.Where(p => p.Seller_ID == seller.ID).ToList();
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
                Seller seller = (Seller)Session["seller"];
                brand.Seller_ID = seller.ID;
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
            Seller seller = (Seller)Session["seller"];

            if (seller == null)
            {
                return RedirectToAction("Login", "Index");
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
            Seller seller = (Seller)Session["seller"];

            if (seller == null)
            {
                return RedirectToAction("Login", "Index");
            }

            brand.Seller_ID = seller.ID;

            if (brand.Seller_ID != seller.ID)
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

            Seller seller = (Seller)Session["seller"];
            Brand brand = db.Brand.FirstOrDefault(b => b.ID == id && b.Seller_ID == seller.ID);

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

            Seller seller = (Seller)Session["seller"];
            Brand brand = db.Brand.FirstOrDefault(b => b.ID == id && b.Seller_ID == seller.ID);

            if (brand == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            brand.IsDeleted = false;
            brand.IsActive = true;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = (Seller)Session["seller"];
            Brand brand = db.Brand.FirstOrDefault(b => b.ID == id && b.Seller_ID == seller.ID);

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
