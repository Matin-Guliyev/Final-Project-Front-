using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(1000)]
        public string Subject { get; set; }
        [Required, StringLength(2000)]
       
        public string sms { get; set; }
    }
}
