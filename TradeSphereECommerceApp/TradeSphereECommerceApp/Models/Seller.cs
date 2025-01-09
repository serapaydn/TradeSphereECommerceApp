using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TradeSphereECommerceApp.Models
{
    public class Seller
    {
        public Seller()
        {
            IsActive = true;
            IsDeleted = false;
            CreationTime = DateTime.Now;
            SellerCode = GenerateSellerCode();
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "Mağaza adı zorunludur")]
        [StringLength(100)]
        [Display(Name = "Mağaza Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(maximumLength: 75, ErrorMessage = "Bu alan en fazla 75 karakter olabilir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [Display(Name = "E-posta")]
        [StringLength(maximumLength: 150, ErrorMessage = "Bu alan en fazla 150 karakter olabilir")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [Display(Name = "Şifre")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Bu alan en fazla 6-20 karakter olabilir")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [StringLength(15)]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        [StringLength(50)]
        [Required(ErrorMessage = "Satıcı Kodu zorunludur")]
        public string SellerCode { get; set; }
        private string GenerateSellerCode()
        {
            return "TS-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
        [Display(Name = "Bayi Tipi")]
        public string SellerType { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = true;
        public ICollection<Product> Products { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}