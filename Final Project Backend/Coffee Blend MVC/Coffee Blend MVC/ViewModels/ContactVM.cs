using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.ViewModels
{
    public class ContactVM
    {
        public string Name { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email{get;set;}
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
