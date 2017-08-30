using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AthenasSpider.Models
{
    public class ProductModel
    {
        [Display(Name = "ItemId")]
        public int ItemId { get; set; }

        [Required]
        [Display(Name = "Item")]
        public string Item { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public string ImageUrl { get; set; }
    }
}