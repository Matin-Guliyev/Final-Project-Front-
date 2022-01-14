using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int No { get; set; }
        public double TotalPrice { get; set; }
        public bool IsApproved { get; set; }
        public string ApproveNote { get; set; }
        [Required]

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsDeleted { get; set; }
      
        public List<OrderItem> OrderItems { get; set; }
        public int BillingAddressId { get; set; }
        public BillingAddress BillingAddress { get; set; }
    }
}
