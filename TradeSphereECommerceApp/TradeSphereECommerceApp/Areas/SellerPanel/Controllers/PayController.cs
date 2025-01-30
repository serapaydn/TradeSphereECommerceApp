using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Areas.SellerPanel.Data;
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

            if (FileUploadApiController.TempProducts == null || !FileUploadApiController.TempProducts.Any())
            {
                TempData["Warning"] = "Geçici ürün listesi boş.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            var selectedProducts = FileUploadApiController.TempProducts
                .Where(p => selectedProduct.Contains(p.ID))
                .Select(p => new SelectedProductDto
                {
                    ProductName = p.Name,
                    Barcode = p.Barcode,
                    Price = p.Price,
                    GoldPrice = p.GoldPrice,
                    SilverPrice = p.SilverPrice,
                    BronzePrice = p.BronzePrice,
                    Stock = p.Stock
                })
                .ToList();

            if (!selectedProducts.Any())
            {
                TempData["Warning"] = "Seçilen ürünler bulunamadı.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            double totalAmount = selectedProducts.Sum(p => p.Price * p.Stock);
            ViewBag.SelectedProducts = selectedProducts;
            ViewBag.TotalAmount = totalAmount;

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));


            return View(new PaymentViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> Payment(PaymentViewModel model, int[] selectedProduct)
        {
            Seller seller = (Seller)Session["seller"];
            if (seller == null)
            {
                return RedirectToAction("Login", "Seller");
            }

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));


            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurunuz.";
                return RedirectToAction("Index", new { selectedProduct });
            }

            if (selectedProduct == null || selectedProduct.Length == 0)
            {
                ViewBag.Warning = "Hiçbir ürün seçilmedi.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            //if (FileUploadApiController.TempProducts == null || !FileUploadApiController.TempProducts.Any())
            //{
            //    ViewBag.Warning = "Geçici ürün listesi boş.";
            //    return RedirectToAction("UploadXmlProducts", "Product");
            //}

            var selectedProducts = FileUploadApiController.TempProducts
                 .Where(p => selectedProduct.Contains(p.ID))
                 .Select(p => new SelectedProductDto
                 {
                     ProductName = p.Name,
                     Barcode = p.Barcode,
                     Price = p.Price,
                     GoldPrice = p.GoldPrice,
                     SilverPrice = p.SilverPrice,
                     BronzePrice = p.BronzePrice,
                     Stock = p.Stock
                 })
                 .ToList();

            if (!selectedProducts.Any())
            {
                ViewBag.Warning = "Seçilen ürünler bulunamadı.";
                return RedirectToAction("UploadXmlProducts", "Product");
            }

            double totalAmount = selectedProducts.Sum(p => p.Price * p.Stock);
            string totalAmountStr = totalAmount.ToString("F2").Replace(",", ".");

            try
            {
                string apiUrl = "https://localhost:44385/api/sellerpayment/payment";
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && responseString == "\"201\"")
                    {
                        foreach (var productDto in selectedProducts)
                        {
                            var product = new Product
                            {
                                Name = productDto.ProductName,
                                Barcode = productDto.Barcode,
                                Price = productDto.Price,
                                GoldPrice = productDto.GoldPrice,
                                SilverPrice = productDto.SilverPrice,
                                BronzePrice = productDto.BronzePrice,
                                Stock = productDto.Stock
                            };

                            db.Products.Add(product);
                        }

                        db.SaveChanges();
                        FileUploadApiController.TempProducts.RemoveAll(p => selectedProduct.Contains(p.ID)); 
                        ViewBag.Success = "Ödeme başarıyla tamamlandı ve ürünler sisteme eklendi.";
                        return RedirectToAction("Index", "Product");
                    }

                    switch (responseString)
                    {
                        case "\"801\"":
                            ViewBag.Error = "CVV Hatalı";
                            break;
                        case "\"901\"":
                            ViewBag.Error = "Kart Bulunamadı";
                            break;
                        case "\"701\"":
                            ViewBag.Error = "Satıcı Sistem hatası";
                            break;
                        case "\"601\"":
                            ViewBag.Error = "Satıcı Aktif Değil";
                            break;
                        case "\"501\"":
                            ViewBag.Error = "Son Kullanma Tarihi Geçersiz";
                            break;
                        case "\"401\"":
                            ViewBag.Error = "Kart Kullanıma Kapalı";
                            break;
                        case "\"301\"":
                            ViewBag.Error = "Bakiye Yetersiz";
                            break;
                        default:
                            ViewBag.Error = "Bilinmeyen bir hata oluştu.";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index", new { selectedProduct });
        }
    }
}



    

    
