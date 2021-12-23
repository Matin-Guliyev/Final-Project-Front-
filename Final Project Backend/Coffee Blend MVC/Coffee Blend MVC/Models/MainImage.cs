using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class MainImage : BaseEntity
    {
        
        [StringLength(255)]
        public string Image { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Head { get; set; }
        
        [Required]
        public string Text { get; set; }
    }
}
