using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Areas.SellerPanel.Controllers
{

    [RoutePrefix("api/fileupload")]
    public class FileUploadApiController : ApiController
    {
        public static List<Product> TempProducts = new List<Product>();
        public static int TempProductIdCounter = 1;

        TradeSphereDBModel db = new TradeSphereDBModel();

        [HttpPost]
        [Route("uploadxml")]
        public IHttpActionResult UploadXmlFromFile()
        {
            string filePath = @"C:/Users/serap/Documents/Products.xml";

            if (File.Exists(filePath))
            {
                try
                {
                    string xmlContent = File.ReadAllText(filePath);
                    var xDocument = XDocument.Parse(xmlContent);

                    TempProducts = (from p in xDocument.Descendants("Product")
                                    select new Product
                                    {
                                        ID = TempProductIdCounter++,
                                        Name = p.Element("Name")?.Value,
                                        Barcode = p.Element("Barcode")?.Value,
                                        Price = (double)decimal.Parse(p.Element("Price")?.Value ?? "0"),
                                        GoldPrice = (double)decimal.Parse(p.Element("GoldPrice")?.Value ?? "0"),
                                        SilverPrice = (double)decimal.Parse(p.Element("SilverPrice")?.Value ?? "0"),
                                        BronzePrice = (double)decimal.Parse(p.Element("BronzePrice")?.Value ?? "0"),
                                        Stock = short.Parse(p.Element("Stock")?.Value ?? "0"),
                                        CreationTime = DateTime.Now,
                                        IsDeleted = false
                                    }).ToList();

                    return Ok(TempProducts);
                }
                catch (Exception ex)
                {
                    return InternalServerError(new Exception($"XML dosyası işlenirken bir hata oluştu: {ex.Message}"));
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("confirmproducts")]
        public IHttpActionResult ConfirmProducts(List<Product> products, string sellerType)
        {
            if (products == null || !products.Any())
            {
                return BadRequest("Hiçbir ürün onaylanmadı.");
            }

            foreach (var product in products)
            {
                switch (sellerType.ToLower())
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
                        return BadRequest("Geçersiz satıcı türü.");
                }
            }

            db.Products.AddRange(products);
            db.SaveChanges();
            TempProducts.Clear();
            return Ok("Ürünler başarıyla eklendi.");
        }

        [HttpGet]
        [Route("gettempproducts")]
        public IHttpActionResult GetTempProducts()
        {
            return Ok(TempProducts);
        }
    }
}
