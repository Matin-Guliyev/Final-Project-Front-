using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class BillingAddress
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(300)]
        public string LastName { get; set; }
    
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAdress2 { get; set; }
        public string Town { get; set; }
        public int ZipCode { get; set; }
        [Required]
        [StringLength(300)]
        public string Email { get; set; }
        [Required]
        [StringLength(300)]
        public string Phone { get; set; }
   

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
