using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Data.ViewModels
{
    public class ShoppingCartDto
    {

        public int ID { get; set; }
        public int Product_ID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}