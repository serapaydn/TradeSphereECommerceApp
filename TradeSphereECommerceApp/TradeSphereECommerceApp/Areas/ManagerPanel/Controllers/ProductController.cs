using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Controllers
{
    [ManagerAuthorizationFilter]
    public class ProductController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();

        public ActionResult Index()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }
            List<Product> products = db.Products.Where(p => p.Manager_ID == manager.ID && p.IsDeleted == false).ToList();
            return View(products);
        }

        public ActionResult AllIndex()
        {
            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }
            List<Product> products = db.Products.Where(p => p.Manager_ID == manager.ID).ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categories.Where(c => c.IsActive && !c.IsDeleted), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brand.Where(c => c.IsActive && !c.IsDeleted), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product model, HttpPostedFileBase productImage)
        {
            try
            {
                model.CreationTime = DateTime.Now;
                model.IsDeleted = false;
                model.Manager_ID = (Session["manager"] as Manager).ID;

                bool imageIsValid = false;
                if (productImage != null)
                {
                    FileInfo fi = new FileInfo(productImage.FileName);
                    if (fi.Extension == ".jpg" || fi.Extension == ".png" || fi.Extension == ".jpeg")
                    {
                        imageIsValid = true;
                        Guid filename = Guid.NewGuid();
                        string fullname = filename + fi.Extension;
                        productImage.SaveAs(Server.MapPath("~/Assets/ProductImages/" + fullname));
                        model.Image = fullname;
                    }
                }
                else
                {
                    imageIsValid = true;
                    model.Image = "none.png";
                }

                if (imageIsValid)
                {
                    db.Products.Add(model);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            Manager manager = (Manager)Session["manager"];
            if (manager == null)
            {
                return RedirectToAction("Login", "Manager");
            }

            Product product = db.Products.Find(id);
            if (product == null || product.Manager_ID != manager.ID)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            ViewBag.Category_ID = new SelectList(db.Categories.Where(c => c.IsActive && !c.IsDeleted), "ID", "Name", product.Category_ID);
            ViewBag.Brand_ID = new SelectList(db.Brand.Where(c => c.IsActive && !c.IsDeleted), "ID", "Name", product.Brand_ID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product model, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model).State = EntityState.Modified;

                    if (productImage != null)
                    {
                        bool imageIsValid = false;
                        FileInfo fi = new FileInfo(productImage.FileName);
                        if (fi.Extension == ".jpg" || fi.Extension == ".png" || fi.Extension == ".jpeg")
                        {
                            imageIsValid = true;
                            Guid filename = Guid.NewGuid();
                            string fullname = filename + fi.Extension;
                            productImage.SaveAs(Server.MapPath("~/Assets/ProductImages/" + fullname));
                            model.Image = fullname;
                        }

                        if (imageIsValid)
                        {
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Product");
            }

            Manager manager = (Manager)Session["manager"];
            Product product = db.Products.FirstOrDefault(p => p.ID == id && p.Manager_ID == manager.ID);

            if (product == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = (Manager)Session["manager"];
            Product product = db.Products.FirstOrDefault(p => p.ID == id && p.Manager_ID == manager.ID);

            if (product == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            product.IsDeleted = true;
            product.IsActive = false;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Product");
            }

            Manager manager = (Manager)Session["manager"];
            Product product = db.Products.FirstOrDefault(p => p.ID == id && p.Manager_ID == manager.ID);

            if (product == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            product.IsDeleted = false;
            product.IsActive = true;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}