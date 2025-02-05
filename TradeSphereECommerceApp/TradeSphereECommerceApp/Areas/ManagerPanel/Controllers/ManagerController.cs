using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Controllers
{
    public class ManagerController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();

        public ActionResult Index()
        {
            return RedirectToAction("Edit");
        }

        public ActionResult Edit()
        {
            Manager manager = Session["manager"] as Manager;

            if (manager == null)
            {
                ViewBag.Warning = "Yönetici oturumunda bir hata oluştu.";
                return RedirectToAction("Index", "Home");
            }

            Manager currentManager = db.Managers.Find(manager.ID);

            if (currentManager != null)
            {
                return View(currentManager);
            }
            else
            {
                ViewBag.Warning = "Yönetici bulunamadı.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manager manager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Manager currentManager = db.Managers.Find(manager.ID);

                    if (currentManager != null)
                    {
                        currentManager.Name = manager.Name;
                        currentManager.Surname = manager.Surname;
                        currentManager.UserName = manager.UserName;
                        currentManager.Mail = manager.Mail;
                        currentManager.MerchantID = manager.MerchantID;

                        if (!string.IsNullOrEmpty(manager.Password))
                        {
                            currentManager.Password = manager.Password;
                        }
                        if (!string.IsNullOrEmpty(manager.MerchantPass))
                        {
                            currentManager.MerchantPass = manager.MerchantPass;
                        }

                        db.Entry(currentManager).State = EntityState.Modified;

                        if (db.SaveChanges() > 0)
                        {
                            Session["manager"] = currentManager;
                            ViewBag.Success = "Profil başarıyla güncellendi.";
                        }
                        else
                        {
                            ViewBag.Warning = "Veritabanı güncellemesi sırasında bir sorun oluştu.";
                        }
                    }
                    else
                    {
                        ViewBag.Warning = "Yönetici bulunamadı.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Warning = "Bir hata oluştu. Lütfen tekrar deneyin.";
                    System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
                }
            }
            else
            {
                ViewBag.Warning = "Girilen bilgilerde hata var. Lütfen kontrol edip tekrar deneyin.";
            }

            return View(manager);
        }
    }
}
