using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TradeSphereECommerceApp.Data.ViewModels;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay
        TradeSphereDBModel db = new TradeSphereDBModel();


        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Payment(PaymentViewModel model)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int userId = ((Member)Session["user"]).ID;
            List<ShoppingCart> cart = db.ShoppingCarts.Where(x => x.Member_ID == userId).ToList();
            TempData["cart"] = cart;

            ViewBag.Months = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Now.Year, 20));

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (cart == null || !cart.Any())
            {
                ViewBag.Message = "Sepetiniz boş.";
                return View("Index", model);
            }

            model.Cart = cart.Select(c => new ShoppingCartDto
            {
                ID = c.ID,
                Product_ID = c.Product_ID,
                ProductName = c.Product.Name,
                Price = c.Product.Price,
                Quantity = c.Quantity,
                TotalPrice = c.Quantity * c.Product.Price
            }).ToList();

            try
            {
                string apiUrl = "https://localhost:44385/api/payment/payment";
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        if (responseString.Trim() == "201")
                        {
                            foreach (var item in cart)
                            {
                                db.ShoppingCarts.Remove(item);
                            }
                            db.SaveChanges();
                            return RedirectToAction("PaymentSuccess","Pay");
                        }
                        else
                        {

                            switch (responseString)
                            {
                                case "801":
                                    ViewBag.Message = "CVV Hatalı";
                                    break;
                                case "901":
                                    ViewBag.Message = "Kart Bulunamadı";
                                    break;
                                case "701":
                                    ViewBag.Message = "Satıcı Sistem hatası";
                                    break;
                                case "601":
                                    ViewBag.Message = "Satıcı Aktif Değil";
                                    break;
                                case "501":
                                    ViewBag.Message = "Son Kullanma Tarihi Geçersiz";
                                    break;
                                case "401":
                                    ViewBag.Message = "Kart Kullanıma Kapalı";
                                    break;
                                case "301":
                                    ViewBag.Message = "Bakiye Yetersiz";
                                    break;
                                default:
                                    ViewBag.Message = "Bilinmeyen Hata";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = $"Ödeme API hatası: {response.StatusCode} - {responseString}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
        public ActionResult PaymentSuccess()
        {
            return View();
        }
    }
    
}






