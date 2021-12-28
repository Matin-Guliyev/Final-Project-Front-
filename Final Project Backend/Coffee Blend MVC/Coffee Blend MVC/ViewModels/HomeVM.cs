using Coffee_Blend_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.ViewModels
{
    public class HomeVM
    {
        public List<HomeSlider>HomeSliders { get; set; }
        public MainImage MainImages { get; set; }
        public List<FtcoServices> FtcoServices { get; set; }
        public OurMenu OurMenus { get; set; }
        public List<OurMenuImages> OurMenuImages { get; set; }
        public List<Counter> Counters { get; set; }
        public BestSellers BestSellers{ get; set; }
        public List<BestSellersImage> BestSellersImages { get; set; }
        public List<FtcoGallery2> FtcoGallery2s { get; set; }
        public Testimony Testimonies { get; set; }
        public Recent Recents { get; set; }
        public List<RecentImage> RecentImages { get; set; }
        public List<Filter> MainDish { get; set; }
        public List<Filter> Drinks { get; set; }
        public List<Filter> Desserts { get; set; }
    }
}
