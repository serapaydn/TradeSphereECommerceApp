using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class LoginController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string userName, string password)
        {
            Member user = db.Members.FirstOrDefault(m => m.UserName == userName || m.Mail == userName);

            if (user != null && user.Password == password)
            {
                if (user.IsActive)
                {
                    Session["user"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Warning = "Hesabınız askıya alınmış.";
                }
            }
            else
            {
                ViewBag.Warning = "Kullanıcı adı veya şifre yanlış.";
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}