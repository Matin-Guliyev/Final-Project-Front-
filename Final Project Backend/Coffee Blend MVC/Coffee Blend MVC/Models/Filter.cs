using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class Filter:BaseEntity

    {
        [Required]
        [MaxLength(500)]
        public string Images { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Desc { get; set; }
        public double Price { get; set; }
        [Required]
        public  bool IsDrink { get; set; }
        [Required]
        public bool IsMainDish { get; set; }
        [Required]

        public bool IsDessert { get; set; }


    }
}
