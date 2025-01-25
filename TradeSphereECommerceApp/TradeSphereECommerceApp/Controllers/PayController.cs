using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Data.ViewModels;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class PayController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: Pay
        public ActionResult Index(int? productId, int? quantity)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (productId != null && quantity != null)
            {
                Product product = db.Products.Find(productId);
                if (product == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.SingleProduct = new ShoppingCart
                {
                    Product_ID = productId.Value,
                    Product = product,
                    Quantity = quantity.Value,
                    Member_ID = ((Member)Session["user"]).ID
                };
            }

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));

            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int id = ((Member)Session["user"]).ID;
            List<ShoppingCart> cart = db.ShoppingCarts.Where(x => x.Member_ID == id).ToList();
            TempData["cart"] = cart;
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            double toplam = cart.Sum(x => x.Product.Price * x.Quantity);
            string fiyatstr = toplam.ToString().Replace(",", ".");

            var product = cart.FirstOrDefault()?.Product;
            if (product == null)
            {
                ViewBag.Mesaj = "Ürün bulunamadı.";
                return View("Index", model);
            }

            string merchantID = product.Seller?.MerchantID ?? product.Manager?.MerchantID ?? "defaultMerchantID";
            string merchantPass = product.Seller?.MerchantPass ?? product.Manager?.MerchantPass ?? "defaultMerchantPass";

            string apiurl = $"https://localhost:44362/API/Pay?kartNo={model.CardNumber}&ay={model.ExpirationMonth}&yil={model.ExpirationYear}&cvv={model.CVV}&bakiye={fiyatstr}&merchantID={merchantID}&merchantPass={merchantPass}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiurl).Result;
                string stringResp = response.Content.ReadAsStringAsync().Result;

                if (stringResp == "\"201\"")
                {
                    foreach (ShoppingCart item in cart)
                    {
                        db.ShoppingCarts.Remove(item);
                    }

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Ödeme işleminiz başarıyla gerçekleşti!";
                    return RedirectToAction("PaymentSuccess");
                }

                if (stringResp == "\"801\"")
                {
                    ViewBag.Mesaj = "CVV Hatalı";
                }
                else if (stringResp == "\"901\"")
                {
                    ViewBag.Mesaj = "Kart Bulunamadı";
                }
                else if (stringResp == "\"701\"")
                {
                    ViewBag.Mesaj = "Satıcı Sistem hatası";
                }
                else if (stringResp == "\"601\"")
                {
                    ViewBag.Mesaj = "Satıcı Aktif Değil";
                }
                else if (stringResp == "\"501\"")
                {
                    ViewBag.Mesaj = "Son Kullanma Tarihi Geçersiz";
                }
                else if (stringResp == "\"401\"")
                {
                    ViewBag.Mesaj = "Kart Kullanıma Kapalı";
                }
                else if (stringResp == "\"301\"")
                {
                    ViewBag.Mesaj = "Bakiye Yetersiz";
                }
            }

            return View("Index");
        }

    }


}