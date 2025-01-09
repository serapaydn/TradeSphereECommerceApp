using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Areas.ManagerPanel.Data.ViewModels
{
    public class ManagerLoginViewModel
    {
        [Required(ErrorMessage = "Mail alanı zorunludur")]
        [DataType(DataType.EmailAddress)]

        public string Mail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        public string Password { get; set; }
    }
}