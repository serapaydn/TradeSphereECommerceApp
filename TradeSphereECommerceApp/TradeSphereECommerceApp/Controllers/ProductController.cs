using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Product model = db.Products.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.categories = db.Categories.Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

            Member member = Session["user"] as Member;
            if (member != null)
            {
                Favorites favorite = db.Favorites.FirstOrDefault(f => f.Member_ID == member.ID && f.Product_ID == model.ID);
                ViewBag.IsFavorite = favorite != null;
            }
            else
            {
                ViewBag.IsFavorite = false;
            }

            var comments = db.Comments.Where(c => c.Product_ID == model.ID && c.IsActive && !c.IsDeleted)
                                      .OrderByDescending(c => c.CreationTime)
                                      .ToList();
            ViewBag.Comments = comments;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int ProductId, string CommentText)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Index");
            }

            Member member = Session["User"] as Member;
            Comment newComment = new Comment
            {
                Product_ID = ProductId,
                CommentText = CommentText,
                Member_ID = member.ID,
                CreationTime = DateTime.Now
            };

            db.Comments.Add(newComment);
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = ProductId });
        }
    }
}