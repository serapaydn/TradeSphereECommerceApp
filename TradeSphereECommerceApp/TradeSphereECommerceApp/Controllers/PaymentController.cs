using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TradeSphereECommerceApp.Data.ViewModels;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentController : ApiController
    {
        TradeSphereDBModel db = new TradeSphereDBModel();

        [HttpPost]
        [Route("payment")]
        public async Task<IHttpActionResult> Payment(PaymentViewModel model)
        {
            if (model == null || model.Cart == null || !model.Cart.Any())
            {
                return BadRequest("Sepet boş veya geçersiz.");
            }

            double totalAmount = model.Cart.Sum(x => x.Price * x.Quantity);
            model.TotalAmount = totalAmount;
            string fiyatstr = totalAmount.ToString().Replace(",", ".");
            string merchantID = "159753655";
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

                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiurl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseString);

                        if (responseString.Trim() == "201")
                        {
                            foreach (var item in model.Cart)
                            {
                                var cartItem = db.ShoppingCarts.Find(item.ID);
                                if (cartItem != null)
                                {
                                    db.ShoppingCarts.Remove(cartItem);
                                }
                            }
                            await db.SaveChangesAsync();
                            return Ok("201"); 
                        }
                        else
                        {
                            return BadRequest($"Ödeme başarısız: {responseString}");
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







