using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

      
        public async Task<IActionResult> Index()
        {

            List<AppUser> appUsers = await _userManager.Users.ToListAsync();

            List<AppUserVM> appUserVMs = new List<AppUserVM>();

            foreach (AppUser appUser in appUsers)
            {
                AppUserVM appUserVM = new AppUserVM
                {
                    Id = appUser.Id,
                 FullName=appUser.Name,
                    Email = appUser.Email,
                    Role = await _userManager.GetRolesAsync(appUser) != null ? (await _userManager.GetRolesAsync(appUser))[0] : null,
                    UserName = appUser.UserName,
                    IsDeleted = appUser.IsDeleted
                };

                appUserVMs.Add(appUserVM);
            }

            return View(appUserVMs);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(string Id, bool status)
        {
            if (Id == null) return RedirectToAction("Index","error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            appUser.IsDeleted = status;
            await _userManager.UpdateAsync(appUser);

            return RedirectToAction("Index");
        }

     
        public async Task<IActionResult> ChangeRole(string Id)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            AppUserVM appUserVM = new AppUserVM
            {
                Id = appUser.Id,
                FullName = appUser.Name,

                Role = (await _userManager.GetRolesAsync(appUser))[0],
                UserName = appUser.UserName,
                IsDeleted = appUser.IsDeleted,
                Roles = new List<string> { "Admin", "Member", "User" }
            };

            return View(appUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<IActionResult> ChangeRole(string Id, string Roles)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            string oldRole = (await _userManager.GetRolesAsync(appUser))[0];

            await _userManager.RemoveFromRoleAsync(appUser, oldRole);

            await _userManager.AddToRoleAsync(appUser, Roles);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string Id)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            AppUserVM appUserVM = new AppUserVM
            {
                Id = appUser.Id,
                FullName = appUser.Name,

                Role = (await _userManager.GetRolesAsync(appUser))[0],
                UserName = appUser.UserName,
                IsDeleted = appUser.IsDeleted,
                Roles = new List<string> { "Admin", "Member", "User" }
            };

            return View(appUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string Id, string Password)
        {
            if (Id == null) return RedirectToAction("Index", "error");

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null) return RedirectToAction("Index", "error");

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            await _userManager.ResetPasswordAsync(appUser, token, Password);

            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _roleManager.CreateAsync(identityRole);

            await _roleManager.UpdateAsync(identityRole);

            return RedirectToAction("index");
        }
    }
}
