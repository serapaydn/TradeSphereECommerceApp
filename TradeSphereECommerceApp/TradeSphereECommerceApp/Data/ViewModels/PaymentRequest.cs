using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Data.ViewModels
{
    public class PaymentRequest
    {
        internal IEnumerable<object> SelectedProducts;

        public PaymentViewModel Model { get; set; }

        public int[] SelectedProduct { get; set; }
    }
}