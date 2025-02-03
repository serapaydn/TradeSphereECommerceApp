using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            List<Order> orders = db.Order.Where(o => o.Seller_ID == seller.ID && o.IsDeleted == false).ToList();

            return View(orders);
        }

        public ActionResult AllIndex()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            List<Order> orders = db.Order.Where(o => o.Seller_ID == seller.ID).ToList();

            return View(orders);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Order");
            }

            Order order = db.Order.Find(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            if (order != null)
            {
                order.IsDeleted = true;
                order.IsActive = false; 
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Order");
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
    }
}
