using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.ViewModels
{
    public class LoginVM
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string Password { get; set; }
       
    }
}
