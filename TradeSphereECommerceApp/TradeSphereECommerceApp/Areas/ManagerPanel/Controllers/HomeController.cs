using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthorizationFilter]
    public class HomeController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/Home
        public ActionResult Index()
        {
            Manager manager = (Manager)Session["manager"];

            if (manager == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.ProductCount = db.Products.Where(x => x.IsDeleted == false && x.Manager_ID == manager.ID).Count();
            ViewBag.CategoryCount = db.Categories.Where(x => x.IsDeleted == false).Count();
            ViewBag.BrandCount = db.Brand.Where(x => x.IsDeleted == false && x.Manager_ID == manager.ID).Count();
            ViewBag.MemberCount = db.Members.Where(x => x.IsDeleted == false).Count();
            ViewBag.SellerCount = db.Sellers.Where(x => x.IsDeleted == false).Count();
            ViewBag.CommentCount = db.Comments.Where(x => x.IsDeleted == false).Count();
            ViewBag.OrderCount = db.Order.Where(x => x.IsDeleted == false && x.Manager_ID == manager.ID).Count();
            ViewBag.OrderDetailCount = db.OrderDetail.Where(x => x.IsDeleted == false && x.Manager_ID == manager.ID).Count();
            ViewBag.Manager = manager;

            return View(manager);
        }
    }
}
