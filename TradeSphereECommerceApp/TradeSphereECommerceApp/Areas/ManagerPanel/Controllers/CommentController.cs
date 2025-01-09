using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthorizationFilter]
    public class CommentController : Controller
    {
        // GET: ManagerPanel/Comment
        public ActionResult Index()
        {
            return View();
        }
    }
}