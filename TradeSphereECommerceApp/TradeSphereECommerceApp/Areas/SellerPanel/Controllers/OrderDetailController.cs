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

            List<OrderDetail> orderdetail = db.OrderDetail.Where(o => o.Seller_ID == seller.ID && o.IsDeleted == false).ToList();

            return View(orderdetail);
        }
        public ActionResult AllIndex()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            List<OrderDetail> orderdetail = db.OrderDetail.Where(o => o.Seller_ID == seller.ID).ToList();
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderdetail = db.OrderDetail.Find(id);

            if (orderdetail == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            orderdetail.IsDeleted = true;
            orderdetail.IsActive = false;
            db.Entry(orderdetail).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
