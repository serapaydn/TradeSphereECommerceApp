using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Models
{
    public class Member
    {
        public Member()
        {
            CreationTime = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [StringLength(maximumLength: 75, ErrorMessage = "Bu alan en fazla 75 karakter olabilir")]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alan zorunludur")]
        [Display(Name = "Soyisim")]
        [StringLength(maximumLength: 75, ErrorMessage = "Bu alan en fazla 75 karakter olabilir")]
        public string Surname { get; set; }

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

        [Display(Name = "Kayıt tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Son giriş tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastLoginTime { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}