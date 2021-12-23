using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class MenuController : Controller
    {

        public AppDbContext _context { get; }

        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public  async Task<IActionResult>  Index()
        {

            MenuVM MenuVM = new MenuVM
            {

            };
            return View();
        }
    }
}
