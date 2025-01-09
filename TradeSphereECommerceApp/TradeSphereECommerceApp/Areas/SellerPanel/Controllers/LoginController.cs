using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.SellerPanel.Data.ViewModels;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    public class LoginController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();

        // GET: SellerPanel/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SellerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Seller s = db.Sellers.FirstOrDefault(x => x.Mail == model.Mail && x.Password == model.Password);
                if (s != null)
                {
                    if (s.IsActive)
                    {
                        Session["seller"] = s;
                        Session["manager"] = null;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.warning = "Hesabınız sistem yöneticisi tarafından askıya alınmıştır";
                    }
                }
                else
                {
                    ViewBag.warning = "Kullanıcı bulunamadı";
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session["seller"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}