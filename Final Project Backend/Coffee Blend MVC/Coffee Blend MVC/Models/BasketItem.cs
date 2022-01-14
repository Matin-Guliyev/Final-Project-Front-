using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class BasketItem
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public bool IsDeleted { get; set; }
        public BestSellersImage BestSellersImage { get; set; }
        public int BestSellersImageId { get; set; }
    }
}