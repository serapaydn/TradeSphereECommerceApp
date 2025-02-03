﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Models
{
    public class OrderDetail
    {
        public OrderDetail()
        {
            IsDeleted = false;
        }
        [Key]
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public bool IsDeleted { get; set; }
        public int Seller_ID { get; internal set; }
        public int Manager_ID { get; internal set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; internal set; }
    }
}