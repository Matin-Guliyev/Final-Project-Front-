using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class ContactController : Controller
    {
        public readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(ContactVM contactVM)
        {
            if (!ModelState.IsValid) return View(contactVM);
            Message message = new Message
            {
                Name = contactVM.Name,
                Email = contactVM.Email,
                sms = contactVM.Message,
                Subject = contactVM.Subject,
            
            };
            if (message == null) return RedirectToAction("Index", "error");
            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
