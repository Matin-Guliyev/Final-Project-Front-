using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class HomeSlider:BaseEntity
    {
        
        [Required, StringLength(255)]
        public string Images { get; set; }
        [Required, StringLength(255)]
        public string Text1 { get; set; }
        [StringLength(255)]
        public string Text2 { get; set; }
        [StringLength(255)]
        public string Text3 { get; set; }
        [StringLength(255)]
        public  string Button1 { get; set; }
        [StringLength(255)]
        public string Button2 { get; set; }
       public bool IsDeleted { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }


    }
}
