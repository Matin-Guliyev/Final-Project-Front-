using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CheckoutController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<BasketItem> basketItems = await _context.BasketItems.Include(x => x.AppUser)
                .Where(x => x.AppUser.UserName == User.Identity.Name)
             
                .ToListAsync();

         
            ViewBag.BasktetProduct = basketItems;


            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(BillingAddress billingAddress)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "error");
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appUser == null) return RedirectToAction("Index", "Home");
            List<BasketItem> basketItems = await _context.BasketItems
                
                .Where(x => x.AppUserId == appUser.Id).ToListAsync();
            if (basketItems.Count <= 0)
            {
                TempData["Error"] = "Sebet bosdur Sifaris bas tutmadi";
                return RedirectToAction("index", "Home");
            }
            int? OrderNo = _context.Orders.OrderByDescending(o => o.Id).FirstOrDefault(x => x.IsDeleted == false)?.No;


            Order order = new Order
            {
                AppUserId = appUser.Id,
                No = (int)(OrderNo == null ? 1000 : ++OrderNo),
                OrderDate = DateTime.UtcNow.AddHours(4),
                BillingAddress = billingAddress

            };
            List<OrderItem> orderItems = new List<OrderItem>();
            int OrderItemNo = 0;
            double Total = 0;
            foreach (BasketItem basket in basketItems)
            {
                OrderItemNo++;
                OrderItem orderItem = new OrderItem
                {
                    No = OrderItemNo,
                    Count = basketItems.Count, 
                    Price = basket.Price,
                    Title = basket.Title,
                    BestSellersImageId = basket.BestSellersImageId


                };
                Total += (orderItem.Count * orderItem.Price);
                orderItems.Add(orderItem);
                basket.IsDeleted = true;


            }
            order.TotalPrice = Total;
            order.OrderItems = orderItems;
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Sifarisiniz Ugurla Tamamlandi";

            return RedirectToAction("index", "Home");
        }
       

        public IActionResult OrderList()
        {
            List<Order> orders = _context.Orders.Include(x => x.OrderItems)
                .ThenInclude(x => x.BestSellersImage)      
                .Where(x => x.AppUser.UserName == User.Identity.Name).ToList();
            return View(orders);

        }
    }
}
