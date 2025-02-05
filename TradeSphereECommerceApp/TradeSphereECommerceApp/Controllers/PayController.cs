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

                ViewBag.TotalAmount = product.Price * quantity.Value;
            }
            else
            {
                int userId = ((Member)Session["user"]).ID;
                List<ShoppingCart> cart = db.ShoppingCarts.Where(x => x.Member_ID == userId).ToList();

                if (!cart.Any())
                {
                    ViewBag.Message = "Sepetiniz boş.";
                }
                else
                {
                    ViewBag.Cart = cart;
                    ViewBag.TotalAmount = cart.Sum(item => item.Product.Price * item.Quantity);
                }
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
                Price = (double)c.Product.Price,
                Quantity = c.Quantity,
                TotalPrice = (double)(c.Quantity * c.Product.Price)
            }).ToList();

            try
            {
                string apiUrl = "https://localhost:44385/api/payment/payment";
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    string responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseString);
                    if (response.IsSuccessStatusCode)
                    {

                        if (responseString.Trim() == "201")
                        {
                            try
                            {
                                Order newOrder = new Order
                                {
                                    MemberID = userId.ToString(),
                                    OrderDate = DateTime.Now,
                                    RequiredDate = DateTime.Now.AddDays(7),
                                    ShippedDate = DateTime.Now.AddDays(3),
                                    Freight = 0,
                                    ShipName = "Default Name",
                                    ShipAddress = "Default Address",
                                    IsActive = true,
                                    IsDeleted = false
                                };

                                db.Order.Add(newOrder);
                                db.SaveChanges();

                                foreach (var item in cart)
                                {
                                    db.ShoppingCarts.Remove(item);
                                }
                                db.SaveChanges();

                                return RedirectToAction("PaymentSuccess", "Pay");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Hata oluştu: " + ex.Message);
                                TempData["ErrorMessage"] = "Sipariş kaydedilirken hata oluştu!";
                                return RedirectToAction("Index");
                            }
                        }

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








