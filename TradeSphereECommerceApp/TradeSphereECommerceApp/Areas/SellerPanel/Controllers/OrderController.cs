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
    public class OrderController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/Order
        public ActionResult Index()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            List<Order> orders = db.Order.Where(o => o.Seller_ID == seller.ID).ToList();

            return View(orders);
        }
    }
}