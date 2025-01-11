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
    public class MemberController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: ManagerPanel/Member
        public ActionResult Index()
        {
            return View(db.Members.Where(b => b.IsDeleted == false).ToList());

        }
        public ActionResult AllIndex()
        {
            return View(db.Members.ToList());
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(member);
        }
        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Member");
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            member.IsDeleted = false;
            db.Entry(member).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            member.IsDeleted = true;
            member.IsActive = false;
            db.Entry(member).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}