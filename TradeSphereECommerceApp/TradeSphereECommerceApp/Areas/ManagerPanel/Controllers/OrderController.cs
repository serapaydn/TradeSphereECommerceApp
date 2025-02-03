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
    public class OrderController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/Order
        public ActionResult Index()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }

            List<Order> orders = db.Order.Where(o => o.Manager_ID == manager.ID && o.IsDeleted == false).ToList();

            return View(orders);
        }
        public ActionResult AllIndex()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }
            List<Order> orders = db.Order.Where(p => p.Manager_ID == manager.ID).ToList();
            return View(orders);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Order");
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(order);
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Order");
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            order.IsDeleted = false;
            order.IsActive = true;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            order.IsDeleted = true;
            order.IsActive = false;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}