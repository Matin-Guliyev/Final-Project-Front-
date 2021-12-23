using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class BestSellersImage:BaseEntity
    {
        public string Images { get; set; }
        public string Head { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }
    }
}
