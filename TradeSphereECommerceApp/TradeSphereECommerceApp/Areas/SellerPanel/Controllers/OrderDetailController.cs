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
    public class OrderDetailController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/OrderDetail
        public ActionResult Index()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            List<OrderDetail> orderdetail = db.OrderDetail.Where(o => o.Seller_ID == seller.ID).ToList();

            return View(orderdetail);
        }
    }
}