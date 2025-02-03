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
    public class CommentController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/Comment
        public ActionResult Index()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }
            List<Comment> comments = db.Comments.Where(c => c.Product.Seller_ID == seller.ID && c.IsDeleted == false).ToList();
            return View(comments);
        }

        public ActionResult AllIndex()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            List<Comment> comments = db.Comments.Where(c => c.Product.Seller_ID == seller.ID).ToList();
            return View(comments);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }


            Comment comment = db.Comments.FirstOrDefault(c => c.ID == id);
            if (comment == null || comment.Product.Seller_ID != ((Seller)Session["seller"]).ID)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = (Seller)Session["seller"];
            Comment comment = db.Comments.FirstOrDefault(c => c.ID == id && c.Product.Seller_ID == seller.ID);

            if (comment == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            comment.IsDeleted = true;
            comment.IsActive = false;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Seller seller = (Seller)Session["seller"];
            Comment comment = db.Comments.FirstOrDefault(c => c.ID == id && c.Product.Seller_ID == seller.ID);

            if (comment == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            comment.IsDeleted = false;
            comment.IsActive = true;
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
