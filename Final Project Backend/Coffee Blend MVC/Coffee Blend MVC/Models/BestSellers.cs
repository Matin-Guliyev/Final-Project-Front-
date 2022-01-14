using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class BestSellers:BaseEntity
    {
        public string Head { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
