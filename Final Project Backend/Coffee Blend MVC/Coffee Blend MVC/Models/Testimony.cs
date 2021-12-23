using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class Testimony:BaseEntity
    {
        public string Head{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
