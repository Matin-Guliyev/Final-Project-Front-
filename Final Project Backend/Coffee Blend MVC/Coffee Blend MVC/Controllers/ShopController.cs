using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            ShopVM ShopVM = new ShopVM
            {
                MainDish = await _context.Filters.Where(x => x.IsMainDish).ToListAsync(),
                Drinks = await _context.Filters.Where(x => x.IsDrink).ToListAsync(),
                Desserts = await _context.Filters.Where(x => x.IsDessert).ToListAsync()
            };
            return View(ShopVM);
        }
    }
}
