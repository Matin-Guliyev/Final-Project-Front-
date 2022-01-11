using Coffee_Blend_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.ViewModels
{
    public class MenuVM
    {
        public List<Menu> Menus { get; set; }
        public List<MenuHead> MenuHeads { get; set; }
        public List<Filter> MainDish { get; set; }
        public List<Filter> Drinks { get; set; }
        public List<Filter> Desserts { get; set; }
    }
}
