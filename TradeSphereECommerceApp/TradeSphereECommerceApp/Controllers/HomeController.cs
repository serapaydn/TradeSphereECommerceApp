using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            int pageSize = 12;
            var products = db.Products.Where(p => p.IsDeleted == false && p.IsActive == true).OrderBy(p => p.Name).ToList();
            int totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.categories = db.Categories.Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(pagedProducts);
        }

    }
}