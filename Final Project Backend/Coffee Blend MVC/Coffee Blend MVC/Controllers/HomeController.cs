using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext  _context { get; }

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                HomeSliders= await _context.HomeSliders.ToListAsync(),
                MainImages = await _context.MainImages.FirstOrDefaultAsync(),
                FtcoServices=await _context.FtcoServices.ToListAsync(),
                OurMenus=await _context.OurMenus.FirstOrDefaultAsync(),
                OurMenuImages=await _context.OurMenuImages.ToListAsync(),
                Counters=await _context.Counters.ToListAsync(),
                BestSellers =await _context.BestSellers.FirstOrDefaultAsync(),
                BestSellersImages=await _context.BestSellersImages.ToListAsync(),
                FtcoGallery2s=await _context.FtcoGallery2s.ToListAsync(),
                Testimonies=await _context.Testimonies.FirstOrDefaultAsync(),
                Recents=await _context.Recents.FirstOrDefaultAsync(),
                RecentImages=await _context.RecentImages.ToListAsync()
                
                
            };
            return View(homeVM);
        }


    }
}
