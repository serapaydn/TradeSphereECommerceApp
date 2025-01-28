using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Data.ViewModels
{
    public class SelectedProductDto
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public double TotalPrice => Price * Stock;

        public string Barcode { get; internal set; }
        public double GoldPrice { get; internal set; }
        public double SilverPrice { get; internal set; }
        public double BronzePrice { get; internal set; }
    }
}