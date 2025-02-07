using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Models
{
    public class Product
    {
        public Product()
        {
            IsActive = true;
            IsDeleted = false;
            CreationTime = DateTime.Now;
        }

        public int ID { get; set; }
        public Nullable<int> Category_ID { get; set; }

        [Display(Name = "Kategori")]
        [ForeignKey("Category_ID")]
        public virtual Category Category { get; set; }

        public int? Brand_ID { get; set; }

        [Display(Name = "Marka")]
        [ForeignKey("Brand_ID")]
        public virtual Brand Brand { get; set; }

        public Nullable<int> Manager_ID { get; set; }

        [Display(Name = "Ekleyen")]
        [ForeignKey("Manager_ID")]
        public virtual Manager Manager { get; set; }

        [Display(Name = "Barkod Numarası")]
        [StringLength(maximumLength: 20, ErrorMessage = "En fazla 20 karakter olabilir")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur")]

        [Display(Name = "İsim")]
        [StringLength(maximumLength: 150, ErrorMessage = "En fazla 150 karakter olabilir")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 2000, ErrorMessage = "Açıklama en fazla 2000 karakter olabilir")]
        public string Description { get; set; }

        [Display(Name = "Özet")]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 2000, ErrorMessage = "En fazla 2000 karakter olabilir")]
        public string Summary { get; set; }

        [Display(Name = "Fiyat")]

        public decimal Price { get; set; }
        public decimal GoldPrice { get; set; }
        public decimal SilverPrice { get; set; }
        public decimal BronzePrice { get; set; }

        [Display(Name = "Liste Fiyat")]
        public double ListPrice { get; set; }

        [Display(Name = "Stok")]
        public short Stock { get; set; }

        [Display(Name = "Güvenlik Stoğu")]
        public short ReorderLevel { get; set; }

        [Display(Name = "Ekleme Tarih")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Ürün Resmi")]
        [StringLength(maximumLength: 100)]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public int? Seller_ID { get; internal set; }
        public virtual Seller Seller { get; set; }


    }

}