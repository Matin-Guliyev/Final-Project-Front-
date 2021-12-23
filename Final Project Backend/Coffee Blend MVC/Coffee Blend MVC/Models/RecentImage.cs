using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class RecentImage:BaseEntity
    {
        public string  Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
