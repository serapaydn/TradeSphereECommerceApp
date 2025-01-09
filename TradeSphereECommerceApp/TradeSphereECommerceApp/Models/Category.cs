using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Models
{
    public class Category
    {
        public Category()
        {
            IsActive = true; IsDeleted = false;
        }

        public int ID { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(maximumLength: 150, ErrorMessage = "En fazla 150 karakter olabilir")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 500, ErrorMessage = "En fazla 500 karakter olabilir")]
        public string Description { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public int Seller_ID { get; internal set; }
    }
}