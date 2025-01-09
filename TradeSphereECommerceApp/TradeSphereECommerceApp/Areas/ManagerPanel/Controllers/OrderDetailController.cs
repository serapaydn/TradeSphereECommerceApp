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
    public class OrderDetailController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/OrderDetail
        public ActionResult Index()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }

            List<OrderDetail> orderdetail = db.OrderDetail.Where(o => o.Manager_ID == manager.ID).ToList();

            return View(orderdetail);
        }
    }
}