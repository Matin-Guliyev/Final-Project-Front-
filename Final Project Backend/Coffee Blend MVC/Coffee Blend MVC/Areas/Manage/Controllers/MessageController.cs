using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        #region Message Index

        public IActionResult Index(int page = 1)
        {
            var query = _context.Messages.AsQueryable();
            var Messages = _context.Messages.ToList()
            .Skip((page - 1) * 4).Take(4).ToList();
            ViewBag.TotalPage = Math.Ceiling(query.Count() / 4m);
            ViewBag.SelectPage = page;

            return View(Messages);
        }
        #endregion
        #region Delete messages
        public async Task<IActionResult> DeleteMessage(int id)
        {


            Message message = _context.Messages.Find(id);
            if (message == null) return RedirectToAction("index", "error");
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region ViewMeesage

        public async Task<IActionResult> ViewMessage(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null) return RedirectToAction("index", "Error");

            return View(message);
        }
        #endregion
    }
}
