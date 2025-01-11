using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Models
{
    public class Comment
    {
        public Comment()
        {
            IsActive = true;
            IsDeleted = false;
            CreationTime = DateTime.Now;
        }

        public int ID { get; set; }

        [Required]
        public int Member_ID { get; set; }

        [Required]
        public int Product_ID { get; set; }

        public int? Seller_ID { get; set; }
        public int? Manager_ID { get; set; }

        [Display(Name = "Yorum")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Yorum alanı zorunludur.")]
        [StringLength(500, ErrorMessage = "En fazla 500 karakter olabilir.")]
        public string CommentText { get; set; }

        public DateTime CreationTime { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Member_ID")]
        public virtual Member Member { get; set; }

        [ForeignKey("Product_ID")]
        public virtual Product Product { get; set; }

        [ForeignKey("Seller_ID")]
        public virtual Seller Seller { get; set; }

        [ForeignKey("Manager_ID")]
        public virtual Manager Manager { get; set; }
    }
}