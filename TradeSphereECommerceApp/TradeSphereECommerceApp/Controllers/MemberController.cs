using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class MemberController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int? id)
        {

            Member user = Session["user"] as Member;

            if (user == null)
            {
                ViewBag.Warning = "Kullanıcı oturumunda bir hata oluştu.";
                return RedirectToAction("Index", "Home");
            }

            Member member = db.Members.Find(user.ID);

            if (member != null)
            {
                return View(member);
            }
            else
            {
                ViewBag.Warning = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member user)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Member member = db.Members.Find(user.ID);

                    if (member != null)
                    {
                        member.Name = user.Name;
                        member.Surname = user.Surname;
                        member.UserName = user.UserName;
                        member.Mail = user.Mail;
                        member.Password = user.Password;

                        db.Entry(member).State = EntityState.Modified;

                        if (db.SaveChanges() > 0)
                        {
                            Session["user"] = member;
                            ViewBag.Success = "Profil başarıyla güncellendi.";
                        }
                        else
                        {
                            ViewBag.Warning = "Veritabanı güncellemesi sırasında bir sorun oluştu.";
                        }
                    }
                    else
                    {
                        ViewBag.Warning = "Kullanıcı bulunamadı.";
                    }
                }
                catch
                {
                    ViewBag.Warning = "Bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Warning = "Girilen bilgilerde hata var. Lütfen kontrol edip tekrar deneyin.";
            }

            return View(user);
        }
        public ActionResult Favorites()
        {
            Member member = Session["user"] as Member;
            if (member == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Product> favoriteProducts = db.Favorites
                                    .Where(f => f.Member_ID == member.ID)
                                    .Select(f => f.Product)
                                    .ToList();

            return View(favoriteProducts);
        }
        public ActionResult AddToFavorites(int productId)
        {
            Member member = Session["user"] as Member;

            if (member != null)
            {
                Favorites favorite = db.Favorites.FirstOrDefault(f => f.Member_ID == member.ID && f.Product_ID == productId);

                bool isFavorite;

                if (favorite == null)
                {
                    favorite = new Favorites
                    {
                        Member_ID = member.ID,
                        Product_ID = productId
                    };
                    db.Favorites.Add(favorite);
                    db.SaveChanges();
                    isFavorite = true;
                }
                else
                {
                    db.Favorites.Remove(favorite);
                    db.SaveChanges();
                    isFavorite = false;
                }

                return RedirectToAction("Detail", "Product", new { id = productId, isFavorite });
            }

            ViewBag.Warning = "Lütfen giriş yapın.";
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Comments()
        {
            Member member = Session["user"] as Member;

            if (member == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Comment> memberComments = db.Comments
                                              .Where(c => c.Member_ID == member.ID && c.IsActive && !c.IsDeleted)
                                              .ToList();

            return View(memberComments);
        }
        public ActionResult EditComment(int id)
        {
            Member member = Session["user"] as Member;

            if (member == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Comment comment = db.Comments.Find(id);

            if (comment != null && comment.Member_ID == member.ID && comment.IsActive && !comment.IsDeleted)
            {
                return View(comment);
            }

            return RedirectToAction("Comments");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                Comment existingComment = db.Comments.Find(comment.ID);

                if (existingComment != null)
                {
                    existingComment.CommentText = comment.CommentText;
                    db.Entry(existingComment).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Success = "Yorum başarıyla güncellendi.";
                }
                else
                {
                    ViewBag.Warning = "Yorum bulunamadı.";
                }
            }

            return View(comment);
        }
        public ActionResult DeleteComment(int id)
        {
            Member member = Session["user"] as Member;

            if (member == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Comment comment = db.Comments
                                .Where(c => c.ID == id && c.Member_ID == member.ID && c.IsActive && !c.IsDeleted)
                                .FirstOrDefault();

            if (comment == null)
            {
                ViewBag.Warning = "Yorum bulunamadı veya işlem yetkiniz yok.";
                return RedirectToAction("Comments");
            }

            return View(comment); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            Member member = Session["user"] as Member;

            if (member == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Comment comment = db.Comments.Find(id);

            if (comment != null && comment.Member_ID == member.ID)
            {
                comment.IsDeleted = true;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Yorum başarıyla silindi.";
            }
            else
            {
                TempData["Warning"] = "Yorum bulunamadı veya silme yetkiniz yok.";
            }

            return RedirectToAction("Comments");
        }
    }
}