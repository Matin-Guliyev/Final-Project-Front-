using Coffee_Blend_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.ViewModels
{
    public class AboutVM
    {
        public Testimony Testimonies { get; set; }
        public OurMenu OurMenus { get; set; }
        public List<OurMenuImages> OurMenuImages { get; set; }
        public List<Counter> Counters { get; set; }




    }
}
