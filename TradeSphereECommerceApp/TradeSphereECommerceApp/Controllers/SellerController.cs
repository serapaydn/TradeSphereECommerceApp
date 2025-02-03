using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class SellerController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Seller model)
        {
            if (ModelState.IsValid)
            {
                if (db.Sellers.Any(s => s.Mail == model.Mail))
                {
                    ViewBag.Warning = "Bu e-posta adresi zaten kullanılıyor.";
                    return View(model);
                }

                model.SellerCode = model.SellerCode ?? "TS-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

                model.CreationTime = DateTime.Now;
                model.IsActive = true;
                model.IsDeleted = false;

                db.Sellers.Add(model);
                db.SaveChanges();

                ViewBag.Success = "Kayıt başarılı! Giriş yapabilirsiniz.";
                return RedirectToAction("Index","Login", new { area = "SellerPanel" });
            }

            return View(model);
        }
    }

}