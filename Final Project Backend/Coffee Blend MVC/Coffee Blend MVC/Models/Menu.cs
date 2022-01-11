using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class Menu:BaseEntity
    {
        public string Image { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

    }
}
