using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.ManagerPanel.Filters;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    [SellerAuthorizationFilter]
    public class ProductController : Controller
    {
        // GET: SellerPanel/Product

        TradeSphereDBModel db = new TradeSphereDBModel();
        //private static readonly HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            List<Product> products = db.Products.Where(p => p.Seller_ID == seller.ID && !p.IsDeleted).ToList();
            return View(products);
        }

        public ActionResult AllIndex()
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }
            var product = db.Products.Where(p => p.Seller_ID == seller.ID).ToList();
            return View(product);
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
                Seller seller = (Seller)Session["seller"];
                if (seller == null)
                {
                    return RedirectToAction("Login", "Seller");
                }

                model.Seller_ID = seller.ID;
                model.CreationTime = DateTime.Now;
                model.IsDeleted = false;

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

            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }
            Product product = db.Products.Find(id);
            if (product == null || product.Seller_ID != seller.ID)
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
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }
            Product productToUpdate = db.Products.FirstOrDefault(p => p.ID == id && p.Seller_ID == seller.ID);
            if (productToUpdate == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productToUpdate.Name = model.Name;
                    productToUpdate.Barcode = model.Barcode;
                    productToUpdate.Stock = model.Stock;
                    productToUpdate.ReorderLevel = model.ReorderLevel;
                    productToUpdate.Price = model.Price;
                    productToUpdate.ListPrice = model.ListPrice;
                    productToUpdate.IsActive = model.IsActive;
                    productToUpdate.Category_ID = model.Category_ID;
                    productToUpdate.Brand_ID = model.Brand_ID;
                    productToUpdate.Summary = model.Summary;
                    productToUpdate.Description = model.Description;

                    if (productImage != null)
                    {
                        FileInfo fi = new FileInfo(productImage.FileName);
                        if (fi.Extension == ".jpg" || fi.Extension == ".png" || fi.Extension == ".jpeg")
                        {
                            Guid filename = Guid.NewGuid();
                            string fullname = filename + fi.Extension;
                            productImage.SaveAs(Server.MapPath("~/Assets/ProductImages/" + fullname));
                            productToUpdate.Image = fullname;
                        }
                    }

                    db.Entry(productToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Product");
            }

            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            Product prod = db.Products.SingleOrDefault(p => p.ID == id && p.Seller_ID == seller.ID);
            if (prod == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }
            return View(prod);
        }

        public ActionResult ReDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Product");
            }

            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            Product prod = db.Products.SingleOrDefault(p => p.ID == id && p.Seller_ID == seller.ID);
            if (prod == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            prod.IsDeleted = false;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            Product prod = db.Products.SingleOrDefault(p => p.ID == id && p.Seller_ID == seller.ID);
            if (prod == null)
            {
                return RedirectToAction("NotFound", "SystemMessages");
            }

            prod.IsDeleted = true;
            prod.IsActive = false;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadXmlProducts()
        {
            List<Product> tempProducts = new List<Product>();
            HttpClient client = new HttpClient();

            try
            {
                string apiUrl = "https://localhost:44385/api/fileupload/gettempproducts";

                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    tempProducts = JsonConvert.DeserializeObject<List<Product>>(responseData);
                }
                else
                {
                    ModelState.AddModelError("", "Ürünleri yüklerken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
            }
            finally
            {
                client.Dispose();
            }

            Seller seller = (Seller)Session["seller"];
            ViewBag.SellerType = seller?.SellerType;

            return View(tempProducts);
        }

        [HttpPost]
        public ActionResult ConfirmXmlProducts(int[] selectedProductIds)
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            if (selectedProductIds != null && selectedProductIds.Any())
            {
                var selectedProducts = FileUploadApiController.TempProducts
                    .Where(p => selectedProductIds.Contains(p.ID))
                    .ToList();

                foreach (var product in selectedProducts)
                {

                    product.Seller_ID = seller.ID;
                    switch (seller.SellerType?.ToLower())
                    {
                        case "gold":
                            product.Price = product.GoldPrice;
                            break;
                        case "silver":
                            product.Price = product.SilverPrice;
                            break;
                        case "bronze":
                            product.Price = product.BronzePrice;
                            break;
                        default:
                            product.Price = product.Price;
                            break;
                    }
                    db.Products.Add(product);
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Veritabanı hatası: {ex.Message}";
                    return View("UploadXmlProducts", FileUploadApiController.TempProducts);
                }

                FileUploadApiController.TempProducts.RemoveAll(p => selectedProductIds.Contains(p.ID));
            }

            return RedirectToAction("UploadXmlProducts");
        }
    }
}