using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.Models.ServicesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.ViewModels
{
    public class ServicesVM
    {
        public Discover Discovers { get; set; }
        public ICollection<DiscoverImage> DiscoverImages { get; set; }
        public List<Counter> Counters { get; set; }
        public List<FtcoServices> FtcoServices { get; set; }


    }
}
