using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    public class SystemMessagesController : Controller
    {
        // GET: SellerPanel/SystemMessages
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}