using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TradeSphereECommerceApp.Models
{
    public class Manager
    {
        public Manager()
        {
            CreationTime = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [StringLength(75, ErrorMessage = "Bu alan en fazla 75 karakter olabilir")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [StringLength(75, ErrorMessage = "Bu alan en fazla 75 karakter olabilir")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [StringLength(75, ErrorMessage = "Bu alan en fazla 75 karakter olabilir")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [StringLength(150, ErrorMessage = "Bu alan en fazla 150 karakter olabilir")]
        [Display(Name = "E-posta")]
        public string Mail { get; set; }

        //[Required(ErrorMessage = "Bu Alan zorunludur")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Bu alan en fazla 6-20 karakter olabilir")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Kayıt tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Son giriş tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastLoginTime { get; set; }

        [Display(Name = "Satıcı ID")]
        [StringLength(50)]
        public string MerchantID { get; set; }

        [Display(Name = "Satıcı Şifre")]
        [StringLength(50)]
        public string MerchantPass { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}