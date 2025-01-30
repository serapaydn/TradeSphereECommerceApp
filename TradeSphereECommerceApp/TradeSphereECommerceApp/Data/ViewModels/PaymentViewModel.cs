using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TradeSphereECommerceApp.Models;

namespace TradeSphereECommerceApp.Data.ViewModels
{
    public class PaymentViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        public string FullName { get; set; }

        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Kart numarası 16 haneli olmalıdır")]
        public string CardNumber { get; set; }

        [Display(Name = "Ay")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        public int ExpirationMonth { get; set; }

        [Display(Name = "Yıl")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        public int ExpirationYear { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVC 3 haneli olmalıdır")]
        public string CVV { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        public string Address { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        public string City { get; set; }

        [Display(Name = "Posta Kodu")]
        [Required(ErrorMessage = "Lütfen gerekli alanı doldurunuz")]
        public string PostalCode { get; set; }
        public double TotalAmount { get; set; }
        public List<ShoppingCartDto> Cart { get; set; }
        public int[] SelectedProducts { get; set; }
        //public List<SelectedProductDto> SelectedProducts { get; set; }

    }
}