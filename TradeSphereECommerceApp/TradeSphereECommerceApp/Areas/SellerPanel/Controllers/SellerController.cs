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
    public class SellerController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/Seller
        public ActionResult Index()
        {
            return RedirectToAction("Edit");
        }
        public ActionResult Edit()
        {
            Seller seller = Session["seller"] as Seller;

            if (seller == null)
            {
                ViewBag.Warning = "Satıcı oturumunda bir hata oluştu.";
                return RedirectToAction("Index", "Home");
            }

            Seller currentSeller = db.Sellers.Find(seller.ID);

            if (currentSeller != null)
            {
                return View(currentSeller);
            }
            else
            {
                ViewBag.Warning = "Satıcı bulunamadı.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seller seller)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Seller currentSeller = db.Sellers.Find(seller.ID);

                    if (currentSeller != null)
                    {
                        currentSeller.Name = seller.Name;
                        currentSeller.UserName = seller.UserName;
                        currentSeller.Mail = seller.Mail;
                        currentSeller.Phone = seller.Phone;
                        currentSeller.MerchantID = seller.MerchantID;

                        if (!string.IsNullOrEmpty(seller.Password))
                        {
                            currentSeller.Password = seller.Password;
                        }
                        if (!string.IsNullOrEmpty(seller.MerchantPass))
                        {
                            currentSeller.MerchantPass = seller.MerchantPass;
                        }


                        db.Entry(currentSeller).State = EntityState.Modified;

                        if (db.SaveChanges() > 0)
                        {
                            Session["seller"] = currentSeller;
                            ViewBag.Success = "Profil başarıyla güncellendi.";
                        }
                        else
                        {
                            ViewBag.Warning = "Veritabanı güncellemesi sırasında bir sorun oluştu.";
                        }
                    }
                    else
                    {
                        ViewBag.Warning = "Satıcı bulunamadı.";
                    }
                }
                catch
                {
                    ViewBag.Warning = "Bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Warning = "Girilen bilgilerde hata var. Lütfen kontrol edip tekrar deneyin.";
            }

            return View(seller);
        }
    }
}
