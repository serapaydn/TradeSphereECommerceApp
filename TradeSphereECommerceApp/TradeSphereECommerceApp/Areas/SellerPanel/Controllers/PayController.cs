//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using TradeSphereECommerceApp.Areas.SellerPanel.Data;
//using TradeSphereECommerceApp.Data.ViewModels;
//using TradeSphereECommerceApp.Models;

//namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
//{

//    public class PayController : Controller
//    {
//        TradeSphereDBModel db = new TradeSphereDBModel();
//        // GET: SellerPanel/Pay
//        public ActionResult Index(int[] selectedProduct)
//        {
//            Seller seller = (Seller)Session["seller"];
//            if (seller == null)
//            {
//                return RedirectToAction("Login", "Seller");
//            }

//            if (selectedProduct == null || selectedProduct.Length == 0)
//            {
//                TempData["Warning"] = "Hiçbir ürün seçilmedi.";
//                return RedirectToAction("UploadXmlProducts", "Product");
//            }

//            List<SelectedProductDto> selectedProducts = FileUploadApiController.TempProducts
//                .Where(p => selectedProduct.Contains(p.ID))
//                .Select(p => new SelectedProductDto
//                {
//                    ProductName = p.Name,
//                    Barcode = p.Barcode,
//                    Price = p.Price,
//                    GoldPrice = p.GoldPrice,
//                    SilverPrice = p.SilverPrice,
//                    BronzePrice = p.BronzePrice,
//                    Stock = p.Stock
//                }).ToList();

//            if (!selectedProducts.Any())
//            {
//                TempData["Warning"] = "Seçilen ürünler bulunamadı.";
//                return RedirectToAction("UploadXmlProducts", "Product");
//            }

//            double totalAmount = 0;
//            foreach (var product in selectedProducts)
//            {
//                double unitPrice = product.Price;

//                if (seller.SellerType == "Gold")
//                {
//                    unitPrice = product.GoldPrice;
//                }
//                else if (seller.SellerType == "Silver")
//                {
//                    unitPrice = product.SilverPrice;
//                }
//                else if (seller.SellerType == "Bronze")
//                {
//                    unitPrice = product.BronzePrice;
//                }

//                totalAmount += unitPrice * product.Stock;
//            }

//            List<SelectedProductDto> selectedProductDto = new List<SelectedProductDto>();
//            Session["SelectedProducts"] = selectedProducts;

//            ViewBag.SelectedProducts = selectedProducts;
//            ViewBag.TotalAmount = totalAmount;
//            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
//            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));

//            return View(new PaymentViewModel());
//        }

//        [HttpPost]
//        public async Task<ActionResult> Payment(PaymentViewModel model, int[] selectedProduct)
//        {
//            Seller seller = (Seller)Session["seller"];
//            if (seller == null)
//            {
//                return RedirectToAction("Login", "Seller");
//            }

//            if (selectedProduct == null || selectedProduct.Length == 0)
//            {
//                ViewBag.Warning = "Seçilen ürünler bulunamadı veya oturum süresi dolmuş olabilir.";
//                return RedirectToAction("UploadXmlProducts", "Product");
//            }

//            List<SelectedProductDto> selectedProducts = FileUploadApiController.TempProducts
//                .Where(p => selectedProduct.Contains(p.ID))
//                .Select(p => new SelectedProductDto
//                {
//                    ProductName = p.Name,
//                    Barcode = p.Barcode,
//                    Price = p.Price,
//                    GoldPrice = p.GoldPrice,
//                    SilverPrice = p.SilverPrice,
//                    BronzePrice = p.BronzePrice,
//                    Stock = p.Stock
//                }).ToList();

//            Session["SelectedProducts"] = selectedProducts;

//            if (!selectedProducts.Any())
//            {
//                ViewBag.Warning = "Seçilen ürünler bulunamadı.";
//                return RedirectToAction("UploadXmlProducts", "Product");
//            }

//            double totalAmount = selectedProducts.Sum(p => p.Price * p.Stock);
//            string fiyatstr = totalAmount.ToString("F2").Replace(",", ".");

//            string merchantID = "123456890";
//            string merchantPass = "1234";

//            string apiurl = $"https://localhost:44362/API/Pay?kartNo={model.CardNumber}&ay={model.ExpirationMonth}&yil={model.ExpirationYear}&cvv={model.CVV}&bakiye={fiyatstr}&merchantID={merchantID}&merchantPass={merchantPass}";

//            try
//            {
//                using (HttpClient client = new HttpClient())
//                {
//                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
//                    HttpResponseMessage response = await client.PostAsync(apiurl, content);
//                    string responseString = await response.Content.ReadAsStringAsync();

//                    if (response.IsSuccessStatusCode && responseString == "\"201\"")
//                    {
//                        foreach (var productDto in selectedProducts)
//                        {
//                            var product = new Product
//                            {
//                                Name = productDto.ProductName,
//                                Barcode = productDto.Barcode,
//                                Price = productDto.Price,
//                                GoldPrice = productDto.GoldPrice,
//                                SilverPrice = productDto.SilverPrice,
//                                BronzePrice = productDto.BronzePrice,
//                                Stock = productDto.Stock
//                            };

//                            db.Products.Add(product);
//                        }

//                        db.SaveChanges();
//                        FileUploadApiController.TempProducts.RemoveAll(p => selectedProducts.Any(sp => sp.Barcode == p.Barcode));

//                        ViewBag.Success = "Ödeme başarıyla tamamlandı ve ürünler sisteme eklendi.";
//                        return RedirectToAction("Index", "Product");
//                    }

//                    switch (responseString)
//                    {
//                        case "\"801\"":
//                            ViewBag.Error = "CVV Hatalı";
//                            break;
//                        case "\"901\"":
//                            ViewBag.Error = "Kart Bulunamadı";
//                            break;
//                        case "\"701\"":
//                            ViewBag.Error = "Satıcı Sistem hatası";
//                            break;
//                        case "\"601\"":
//                            ViewBag.Error = "Satıcı Aktif Değil";
//                            break;
//                        case "\"501\"":
//                            ViewBag.Error = "Son Kullanma Tarihi Geçersiz";
//                            break;
//                        case "\"401\"":
//                            ViewBag.Error = "Kart Kullanıma Kapalı";
//                            break;
//                        case "\"301\"":
//                            ViewBag.Error = "Bakiye Yetersiz";
//                            break;
//                        default:
//                            ViewBag.Error = "Bilinmeyen bir hata oluştu.";
//                            break;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                ViewBag.Error = $"Bir hata oluştu: {ex.Message}";
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}












