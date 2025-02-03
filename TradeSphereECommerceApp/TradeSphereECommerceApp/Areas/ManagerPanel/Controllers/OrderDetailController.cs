using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            List<OrderDetail> orderdetail = db.OrderDetail.Where(o => o.Manager_ID == manager.ID && o.IsDeleted == false).ToList();

            return View(orderdetail);
        }
        public ActionResult AllIndex()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }
            List<OrderDetail> orderdetail = db.OrderDetail.Where(o => o.Manager_ID == manager.ID).ToList();
            return View(orderdetail);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderDetail");
            }
            OrderDetail orderdetail = db.OrderDetail.Find(id);
            if (orderdetail == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(orderdetail);
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "OrderDetail");
            }

            OrderDetail orderdetail = db.OrderDetail.Find(id);
            if (orderdetail == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            orderdetail.IsDeleted = false;
            orderdetail.IsActive = true;
            db.Entry(orderdetail).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderdetail = db.OrderDetail.Find(id);
            orderdetail.IsDeleted = true;
            orderdetail.IsActive = false;
            db.Entry(orderdetail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}