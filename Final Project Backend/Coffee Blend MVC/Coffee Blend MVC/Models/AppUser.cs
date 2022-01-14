using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class AppUser: IdentityUser
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        
        //public string Email { get; set; }
        //[DataType(DataType.Password, ErrorMessage = "Password is not valid")]
        //public string Password { get; set; }

    }
}
