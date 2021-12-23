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
    public class ServicesController : Controller
    {
        public AppDbContext _context { get; }

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ServicesVM servicesVM = new ServicesVM
            {
                Discovers = await _context.Discovers.FirstOrDefaultAsync(),
                DiscoverImages = await _context.DiscoverImages.ToListAsync(),
                Counters = await _context.Counters.ToListAsync(),
                FtcoServices = await _context.FtcoServices.ToListAsync()

            };
            return View(servicesVM);
        }
    }
}
