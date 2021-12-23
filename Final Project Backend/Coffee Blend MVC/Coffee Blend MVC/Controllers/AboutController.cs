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
    public class AboutController : Controller
    {
        public AppDbContext _context { get; }

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            AboutVM servicesVM = new AboutVM
            {

                OurMenus = await _context.OurMenus.FirstOrDefaultAsync(),
                OurMenuImages = await _context.OurMenuImages.ToListAsync(),
                Counters = await _context.Counters.ToListAsync(),
                Testimonies = await _context.Testimonies.FirstOrDefaultAsync()


            };
            return View(servicesVM);
        }
    }
}
