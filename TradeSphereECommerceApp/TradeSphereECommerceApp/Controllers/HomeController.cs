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
        public ActionResult Index(int page = 1, int? categoryId = null, string searchTerm = null)
        {
            int pageSize = 12;

            IQueryable<Product> products = db.Products.Where(p => p.IsDeleted == false && p.IsActive == true);

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.Category_ID == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm) || p.Category.Name.Contains(searchTerm) || p.Brand.Name.Contains(searchTerm));
            }

            products = products.OrderBy(p => p.Name);
            int totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.categories = db.Categories.Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            if (!pagedProducts.Any())
            {
                if (categoryId.HasValue && string.IsNullOrEmpty(searchTerm))
                {
                    ViewBag.NoProductsMessage = "Bu kategoriyle ilgili ürün bulunmuyor!";
                }
                else if (!string.IsNullOrEmpty(searchTerm))
                {
                    ViewBag.NoProductsMessage = "Aradığınız kriterlere uygun ürün bulunamadı!";
                }
                else
                {
                    ViewBag.NoProductsMessage = "Ürün bulunmuyor!";
                }
            }

            return View(pagedProducts);
        }

    }
}