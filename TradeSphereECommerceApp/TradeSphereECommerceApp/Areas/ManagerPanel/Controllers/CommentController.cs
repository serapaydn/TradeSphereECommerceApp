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
    public class CommentController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();

        // GET: ManagerPanel/Comment
        public ActionResult Index()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }

            List<Comment> comments = db.Comments.Where(c => c.Product.Manager_ID == manager.ID && c.IsDeleted == false).ToList();
            return View(comments);
        }

        public ActionResult AllIndex()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }

            List<Comment> comments = db.Comments.Where(c => c.Product.Manager_ID == manager.ID).ToList();
            return View(comments);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Manager manager = (Manager)Session["manager"];
            Comment comment = db.Comments.FirstOrDefault(c => c.ID == id && c.Product.Manager_ID == manager.ID);

            if (comment == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = (Manager)Session["manager"];
            Comment comment = db.Comments.FirstOrDefault(c => c.ID == id && c.Product.Manager_ID == manager.ID);

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

            Manager manager = (Manager)Session["manager"];

            Comment comment = db.Comments.Find(id);

            if (comment == null || comment.Product.Manager_ID != manager.ID)
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
