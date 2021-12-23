using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class Counter:BaseEntity
    {
        
        public double Count { get; set; }
        public string Text { get; set; }
        public string Span { get; set; }
    }
}
