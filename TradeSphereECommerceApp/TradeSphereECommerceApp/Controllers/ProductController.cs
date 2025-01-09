using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Product model = db.Products.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.categories = db.Categories.Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

            Member member = Session["user"] as Member;
            if (member != null)
            {
                Favorites favorite = db.Favorites.FirstOrDefault(f => f.Member_ID == member.ID && f.Product_ID == model.ID);
                ViewBag.IsFavorite = favorite != null;
            }
            else
            {
                ViewBag.IsFavorite = false;
            }

            return View(model);
        }
    }
}