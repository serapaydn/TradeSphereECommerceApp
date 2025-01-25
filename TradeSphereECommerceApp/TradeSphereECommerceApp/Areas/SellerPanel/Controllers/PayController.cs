using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Data.ViewModels;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    
    public class PayController : Controller
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        // GET: SellerPanel/Pay
        public ActionResult Index(int[] selectedProduct)
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            if (selectedProduct == null || selectedProduct.Length == 0)
            {
                TempData["Warning"] = "Hiçbir ürün seçilmedi.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            var selectedProducts = FileUploadApiController.TempProducts
                .Where(p => selectedProduct.Contains(p.ID))
                .ToList();

            if (!selectedProducts.Any())
            {
                TempData["Warning"] = "Seçilen ürünler bulunamadı.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));


            double totalAmount = 0;
            foreach (var product in selectedProducts)
            {
                if (seller.SellerType == "Gold")
                {
                    totalAmount += product.GoldPrice * product.Stock;
                }
                else if (seller.SellerType == "Silver")
                {
                    totalAmount += product.SilverPrice * product.Stock;
                }
                else if (seller.SellerType == "Bronze")
                {
                    totalAmount += product.BronzePrice * product.Stock;
                }
                else
                {
                    totalAmount += product.Price * product.Stock; 
                }
            }

            ViewBag.SelectedProducts = selectedProducts;
            ViewBag.TotalAmount = totalAmount;

            return View(new PaymentViewModel());
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model, int[] selectedProductIds)
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen tüm alanları doğru şekilde doldurunuz.";
                return RedirectToAction("Index", new { selectedProductIds });
            }

            var selectedProducts = FileUploadApiController.TempProducts
                .Where(p => selectedProductIds.Contains(p.ID))
                .ToList();

            if (!selectedProducts.Any())
            {
                TempData["Warning"] = "Seçilen ürünler bulunamadı.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            double totalAmount = 0;
            foreach (var product in selectedProducts)
            {
                if (seller.SellerType == "Gold")
                {
                    totalAmount += product.GoldPrice * product.Stock;
                }
                else if (seller.SellerType == "Silver")
                {
                    totalAmount += product.SilverPrice * product.Stock;
                }
                else if (seller.SellerType == "Bronze")
                {
                    totalAmount += product.BronzePrice * product.Stock;
                }
                else
                {
                    totalAmount += product.Price * product.Stock; 
                }
            }
            string totalAmountStr = totalAmount.ToString("F2").Replace(",", ".");
            string merchantID = "123456890";
            string merchantPass = "1234";
            string apiUrl = $"https://localhost:44362/API/Pay?kartNo={model.CardNumber}&ay={model.ExpirationMonth}&yil={model.ExpirationYear}&cvv={model.CVV}&bakiye={totalAmountStr}&merchantID={merchantID}&merchantPass={merchantPass}";

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                string responseString = response.Content.ReadAsStringAsync().Result;

                if (responseString == "\"201\"")
                {
                    foreach (var product in selectedProducts)
                    {
                        db.Products.Add(product);
                    }

                    db.SaveChanges();
                    FileUploadApiController.TempProducts.RemoveAll(p => selectedProductIds.Contains(p.ID));
                    TempData["Success"] = "Ödeme başarıyla tamamlandı ve ürünler sisteme eklendi.";
                    return RedirectToAction("Index", "Product");
                }

                if (responseString == "\"201\"")
                {
                    foreach (var product in selectedProducts)
                    {
                        db.Products.Add(product);
                    }

                    db.SaveChanges();
                    FileUploadApiController.TempProducts.RemoveAll(p => selectedProductIds.Contains(p.ID));
                    TempData["Success"] = "Ödeme başarıyla tamamlandı ve ürünler sisteme eklendi.";
                    return RedirectToAction("Index", "Product");
                }

                if (responseString == "\"801\"")
                {
                    TempData["Error"] = "CVV Hatalı";
                }
                else if (responseString == "\"901\"")
                {
                    TempData["Error"] = "Kart Bulunamadı";
                }
                else if (responseString == "\"701\"")
                {
                    TempData["Error"] = "Satıcı Sistem hatası";
                }
                else if (responseString == "\"601\"")
                {
                    TempData["Error"] = "Satıcı Aktif Değil";
                }
                else if (responseString == "\"501\"")
                {
                    TempData["Error"] = "Son Kullanma Tarihi Geçersiz";
                }
                else if (responseString == "\"401\"")
                {
                    TempData["Error"] = "Kart Kullanıma Kapalı";
                }
                else if (responseString == "\"301\"")
                {
                    TempData["Error"] = "Bakiye Yetersiz";
                }
                else
                {
                    TempData["Error"] = "Bilinmeyen bir hata oluştu.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index", new { selectedProductIds });
        
        }
    }
}
    

    
