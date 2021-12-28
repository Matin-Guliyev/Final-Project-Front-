using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class ShopController : Controller
    {
        public AppDbContext _context { get; }

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            ShopVM MenuVM = new ShopVM
            {

            };
            return View();
        }
    }
}
