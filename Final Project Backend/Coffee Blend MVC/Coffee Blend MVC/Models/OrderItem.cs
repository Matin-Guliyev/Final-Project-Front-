using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int No { get; set; }
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        [Required]
        public int BestSellersImageId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public BestSellersImage BestSellersImage { get; set; }
    }
}
