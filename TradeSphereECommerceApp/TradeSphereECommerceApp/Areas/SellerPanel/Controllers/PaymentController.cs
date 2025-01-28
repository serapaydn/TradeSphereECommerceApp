using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TradeSphereECommerceApp.Data.ViewModels;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{
    [RoutePrefix("api/seller/payment")]
    public class PaymentController : ApiController
    {
        TradeSphereDBModel db = new TradeSphereDBModel();
        [HttpPost]
        [Route("payment")]
        public async Task<IHttpActionResult> Payment(PaymentViewModel model, int[] selectedProduct)
        {

            if (model == null || selectedProduct == null || !selectedProduct.Any())
            {
                return BadRequest("Hiçbir ürün seçilmedi veya geçersiz ürünler.");
            }

            var selectedProducts = FileUploadApiController.TempProducts
                .Where(p => selectedProduct.Contains(p.ID))
                .ToList();

            if (!selectedProducts.Any())
            {
                return BadRequest("Seçilen ürünler bulunamadı.");
            }

            double totalAmount = selectedProducts.Sum(p => p.Price * p.Stock);
            model.TotalAmount = totalAmount;
            string fiyatstr = totalAmount.ToString().Replace(",", "."); 

            string merchantID = "123456890"; 
            string merchantPass = "1234";     

            string apiurl = $"https://localhost:44362/API/Pay?kartNo={model.CardNumber}&ay={model.ExpirationMonth}&yil={model.ExpirationYear}&cvv={model.CVV}&bakiye={fiyatstr}&merchantID={merchantID}&merchantPass={merchantPass}";

            var paymentData = new
            {
                kartNo = model.CardNumber,
                ay = model.ExpirationMonth,
                yil = model.ExpirationYear,
                cvv = model.CVV,
                bakiye = fiyatstr,
                merchantID = merchantID,
                merchantPass = merchantPass
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(paymentData), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiurl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        if (responseContent.Contains("Success"))
                        {
                            foreach (var product in selectedProducts)
                            {
                                db.Products.Add(product);
                            }
                            await db.SaveChangesAsync();
                            FileUploadApiController.TempProducts.RemoveAll(p => selectedProduct.Contains(p.ID));
                            return Ok("Ödeme başarılı, ürünler sisteme eklendi.");
                        }
                        else
                        {
                            return BadRequest($"Ödeme başarısız: {responseContent}");
                        }
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        return BadRequest($"API Hatası: {response.StatusCode} - {errorResponse}");
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(new Exception("Ödeme işlemi sırasında bir hata oluştu.", ex));
                }
            }
        }
    }
}
