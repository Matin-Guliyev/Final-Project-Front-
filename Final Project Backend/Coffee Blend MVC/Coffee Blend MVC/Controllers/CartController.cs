using Coffee_Blend_MVC.DAL;
using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.Services;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermanager;
        private readonly LayoutService _service;
        public CartController(AppDbContext context, UserManager<AppUser> usermanager,LayoutService service)
        {
            _context = context;
            _usermanager = usermanager;
            _service = service;
        }
      
        public async Task<IActionResult> ViewCartBasket()

        {

            List<BasketItem> basket = new List<BasketItem>();
            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _usermanager.FindByNameAsync(User.Identity.Name);
                basket = await _context.BasketItems
                 .ToListAsync();
            }



            return View(basket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewCartBasket(BasketItem basketItems)
        {
            AppUser appUser = await _usermanager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> items = _context.BasketItems.ToList();
           
            await _context.SaveChangesAsync();
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null) return RedirectToAction("Index","error");
            AppUser user = User.Identity.IsAuthenticated ? await _usermanager.FindByNameAsync(User.Identity.Name) : null;
            BestSellersImage snack = _context.BestSellersImages.FirstOrDefault(x => x.Id == id);
            List<BasketVM> basketProducts = new List<BasketVM>();
            if (user == null)
            {
                string basketStr = HttpContext.Request.Cookies["basket"];
                if (basketStr == null)
                {
                    basketProducts.Add(new BasketVM
                    {
                        ProductId = snack.Id,
                        Count = 1,
                        Title = snack.Head,
                        Price = (double)snack.Price,
                        Image = snack.Images
                    });
                }
                else
                {
                    basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(basketStr);
                    BasketVM productBasket = basketProducts.FirstOrDefault(x => x.ProductId == snack.Id);
                    if (productBasket != null)
                    {
                        productBasket.Count++;
                    }
                    else
                    {
                        basketProducts.Add(new BasketVM
                        {
                            ProductId = snack.Id,
                            Count = 1,
                            Title = snack.Head,
                            Price = (double)snack.Price,
                            Image = snack.Images
                        });
                    }
                }
                HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));
            }
            else
            {
                BasketItem productBasket = _context.BasketItems.Where(x => x.AppUserId == user.Id).FirstOrDefault(x => x.BestSellersImageId == id);
                if (productBasket == null)
                {
                    _context.BasketItems.Add(new BasketItem
                    {
                        BestSellersImageId = snack.Id,
                        Count = 1,
                        Title = snack.Head,
                        Price = (double)snack.Price,
                        Image = snack.Images,
                        AppUserId = user.Id
                    });
                }
                else
                {
                    productBasket.Count++;
                }
                _context.SaveChanges();
                basketProducts = _context.BasketItems.Where(x => x.AppUserId == user.Id).Select(x => new BasketVM
                {
                    Count = x.Count,
                    Image = x.Image,
                    ProductId = x.BestSellersImageId,
                    Price = x.Price,
                    Title = x.Title
                }).ToList();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ShowBasket()
        {
            var ProductsSr = HttpContext.Request.Cookies["basket"];
            return Content(ProductsSr);
        }
        public IActionResult RemoveFromBasket(int id)
        {
            BestSellersImage product = _context.BestSellersImages.FirstOrDefault();

            BasketVM basketItem = null;

            if (product == null) return RedirectToAction("index", "error");

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _usermanager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

            }

            List<BasketVM> products = new List<BasketVM>();

            if (member == null)
            {

                string productsStr = HttpContext.Request.Cookies["basket"];
                products = JsonConvert.DeserializeObject<List<BasketVM>>(productsStr);

                basketItem = products.FirstOrDefault(x => x.ProductId == id);


                if (basketItem.Count == 1)
                {

                    products.Remove(basketItem);
                }
                else
                {
                    basketItem.Count--;
                }
                productsStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("basket", productsStr);
            }

            else
            {
                BasketItem memberBasketItem =
               _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id);

                if (memberBasketItem.Count == 1)
                {

                    _context.BasketItems.Remove(memberBasketItem);
                }
                else
                {
                    memberBasketItem.Count--;
                }

                _context.SaveChanges();

                products = _context.BasketItems.Where(x => x.AppUserId == member.Id)
               .Select(x => new BasketVM
               {
                   ProductId = x.Id,
                   Count = x.Count,
                   Title = x.Title,
                   Price = x.Price,
                   Image = x.Image
               }).ToList();

            }
            return RedirectToAction("ViewCartBasket", "Cart");
        }
    }
}

