using Coffee_Blend_MVC.Areas.Manage.Helper;
using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class HomeSliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeSliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        #region Slider Index
        public async Task<IActionResult> Index()
        {
            List<HomeSlider> Sliders = await _context.HomeSliders.Where(x => x.IsDeleted == false).ToListAsync();
            return View(Sliders);
        }
        #endregion
        #region Create Slider

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSlider(HomeSlider sliderVM)
        {
            if (!ModelState.IsValid) return View(sliderVM);

            var photo = sliderVM.Photo;

            if (photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can not be empty");
                return View(sliderVM);
            }

            if (!FileHelper.CheckContent(photo.ContentType, "images/"))
            {
                ModelState.AddModelError("Photo", "Please select image format");
                return View(sliderVM);
            }

            if (!FileHelper.CheckLength(photo.Length, 200))
            {
                ModelState.AddModelError("Photo", "Image size must be less than 200kb");
                return View(sliderVM);
            }

            FileHelper.CreateFile(photo.FileName, _env.WebRootPath, "images", photo);

            HomeSlider slider = new HomeSlider
            {
                
                Text1 = sliderVM.Text1,
                Text2 = sliderVM.Text2,
                Text3 = sliderVM.Text3,
                Button1=sliderVM.Button1,
                Button2=sliderVM.Button2,
                Images = FileHelper.UniqueFileName,
            };

            _context.HomeSliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete slider

        public async Task<IActionResult> DeleteSlider(int id)
        {
            var slider = await _context.HomeSliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            _context.HomeSliders.Remove(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Details Slider
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var slider = await _context.HomeSliders.FindAsync(id);
            if (id == null)
                return NotFound();

            return View(slider);
        }
        #endregion
        public async Task<IActionResult> EditSlider(int id)
        {
            var slider = await _context.HomeSliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            HomeSlider sliderVM = new HomeSlider
            {
                Text1 = slider.Text1,
                Text2=slider.Text2,
                Text3=slider.Text3,
                Button1=slider.Button1,
                Button2=slider.Button2,
                Id=slider.Id
            };

            return View(sliderVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditSlider(HomeSlider sliderVM)
        {
            var slider = await _context.HomeSliders.FirstOrDefaultAsync(s => s.Id == sliderVM.Id);
            if (slider == null) return NotFound();
            if (!ModelState.IsValid) return View(sliderVM);

            var photo = sliderVM.Photo;

            if (photo != null)
            {
                if (!FileHelper.CheckContent(photo.ContentType, "image/"))
                {
                    ModelState.AddModelError("Photo", "Please select image format");
                    return View(sliderVM);
                }

                if (!FileHelper.CheckLength(photo.Length, 200))
                {
                    ModelState.AddModelError("Photo", "Image size must be less than 200kb");
                    return View(sliderVM);
                }

                FileHelper.DeleteFile(slider.Images, _env.WebRootPath, "img/slider");
                FileHelper.CreateFile(sliderVM.Photo.FileName, _env.WebRootPath, "img/slider", sliderVM.Photo);
                slider.Images = FileHelper.UniqueFileName;
            }

            slider.Text1 = sliderVM.Text1;
            slider.Text2 = sliderVM.Text2;
            slider.Text3 = sliderVM.Text3;
            slider.Button1 = sliderVM.Button1;
            slider.Button2 = sliderVM.Button2;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
