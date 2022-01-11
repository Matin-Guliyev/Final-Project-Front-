using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Services
{
    public class LayoutService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public LayoutService(IHttpContextAccessor httpContext, UserManager<AppUser> userManager, AppDbContext context)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _context = context;
        }

        //public async Task<AppUserVM> GetUser()
        //{
        //    AppUserVM appUserVM = null;
        //    if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        AppUser appUser = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);

        //        appUserVM = new AppUserVM
        //        {
        //            Name = appUser.Name,
        //            SurName = appUser.SurName
        //        };
        //    }

        //    return appUserVM;
        //}

        //public async Task<List<BasketProduct>> GetBasket()
        //{
        //    #region Basket With Cookie
        //    //string strBasket = _httpContext.HttpContext.Request.Cookies["basket"];

        //    //List<BasketVM> products = null;

        //    //if (strBasket == null)
        //    //{
        //    //    products = new List<BasketVM>();
        //    //}
        //    //else
        //    //{
        //    //    products = JsonConvert.DeserializeObject<List<BasketVM>>(strBasket);
        //    //}
        //    #endregion

        //    List<BasketProduct> basketProducts = new List<BasketProduct>();

        //    if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        AppUser appUser = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity.Name);

        //        basketProducts = await _context.BasketProducts
        //           .Include(b => b.Product)
        //           .Where(b => b.IsDeleted == false && b.AppUserId == appUser.Id)
        //           .ToListAsync();
        //    }

        //    return basketProducts;
        //}
    }
}


