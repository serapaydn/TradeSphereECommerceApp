using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    [SellerAuthorizationFilter]
    public class HomeController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/Home
        public ActionResult Index()
        {
            Seller seller = (Seller)Session["seller"];

            ViewBag.ProductCount = db.Products.Where(x => x.IsDeleted == false && x.Seller_ID == seller.ID).Count();
            ViewBag.BrandCount = db.Brand.Where(x => x.IsDeleted == false && x.Seller_ID == seller.ID).Count();
            ViewBag.OrderCount = db.Order.Where(x => x.IsDeleted == false && x.Seller_ID == seller.ID).Count();
            ViewBag.OrderDetailCount = db.OrderDetail.Where(x => x.IsDeleted == false && x.Seller_ID == seller.ID).Count();
            ViewBag.CommentCount = db.Comments.Where(x => x.IsDeleted == false).Count();
            ViewBag.Seller = seller;
            return View(seller);
        }
    }
}
