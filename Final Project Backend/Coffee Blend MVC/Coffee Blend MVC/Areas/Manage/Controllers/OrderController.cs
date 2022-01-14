using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Coffee_Blend_MVC.Views.Services.EmailServices;

namespace Coffee_Blend_MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;



        public OrderController(AppDbContext context, IEmailService emailService, IWebHostEnvironment env)
        {
            _context = context;
            _emailService = emailService;
            _env = env;

        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Orders.AsQueryable();
            List<Order> orders = _context.Orders.Include(x => x.OrderItems)

                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.OrderDate)
                 .Include(x => x.BillingAddress).ToList().Skip((page - 1) * 4).Take(4).ToList();
            ViewBag.TotalPage = Math.Ceiling(query.Count() / 4m);
            ViewBag.SelectPage = page;

            return View(orders);
        }
        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems)
                .OrderByDescending(x => x.OrderDate)
                .FirstOrDefault(x => x.Id == id);
            if (order == null) return RedirectToAction("index", "Error");

            return View(order);
        }

        public IActionResult Accept(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems).Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);
            if (order == null) return RedirectToAction("index", "Error");



            _context.SaveChanges();

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "Order", "Orders.html")))
            {
                emailbody = stream.ReadToEnd();
            }

            emailbody = emailbody.Replace("{{total}}", order.TotalPrice.ToString());

            string orderItems = string.Empty;



            emailbody = emailbody.Replace("{{total}}", order.TotalPrice.ToString()).Replace("{{orderItems}}", orderItems);


            _emailService.Send(order.AppUser.Email, "Your Order is accepted", emailbody);
            return RedirectToAction("index");
        }


        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == id);
            if (order == null) return RedirectToAction("index", "Error");


            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Deleted(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems).FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (order == null) return RedirectToAction("index", "Error");
            order.IsDeleted = true;
            _context.SaveChanges();
            return View();


        }
    }
}
