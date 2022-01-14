using Coffee_Blend_MVC.Models;
using Coffee_Blend_MVC.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManeger;
        public readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IWebHostEnvironment _env;
        public AccountController(
             UserManager<AppUser> userManager
             , SignInManager<AppUser> SignInManager,
             RoleManager<IdentityRole> rolemanager,
              IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManeger = SignInManager;
            _rolemanager = rolemanager;
            _env = env;
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("index", "Error");

            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email Ve Ya Shifre Yanlisdir");
                return View(loginVM);
            }

           



            Microsoft.AspNetCore.Identity.SignInResult signinResult = await _signInManeger
                .PasswordSignInAsync(appUser, loginVM.Password, true, true);

            if (signinResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Email Bloklanib");
                return View(loginVM);
            }

            if (!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Ve Ya Shifre Yanlisdir");
                return View(loginVM);
            }

           
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            AppUser appuser = new AppUser
            {
                UserName = registerVM.Name,
                Email = registerVM.Email,

            };
            IdentityResult identityResult = await _userManager.CreateAsync(appuser, registerVM.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View(registerVM);
            };
           
            await _userManager.AddToRoleAsync(appuser, "Member");
            await _signInManeger.SignInAsync(appuser, false);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CoffeeBlend", "shopwisesite@gmail.com"));
            message.To.Add(new MailboxAddress(appuser.Name, appuser.Email));
            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "ConfirmEmail.html")))
            {
                emailbody = stream.ReadToEnd();
            }

            string emailconfirmtoken = await _userManager.GeneratePasswordResetTokenAsync(appuser);

            string url = Url.Action("changepassword", "account", new { id = appuser.Id, token = emailconfirmtoken }, Request.Scheme);

            emailbody = emailbody.Replace("{{UserName}}", $"{appuser.UserName}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("shopwisesite@gmail.com", "shopwise!@123");
            smtp.Send(message);
            smtp.Disconnect(true);

            return RedirectToAction("Login", "account");


        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                AppUser appUser = await _userManager.FindByIdAsync(Id);

                if (appUser == null)
                {
                    return RedirectToAction("Index", "Error");
                }

                IdentityResult identityResult = await _userManager.ConfirmEmailAsync(appUser, token);
                if (!identityResult.Succeeded)
                {

                    return RedirectToAction("Index", "Error");
                }
            }

            return RedirectToAction("login", "account");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManeger.SignOutAsync();
            return RedirectToAction("index", "Home");
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return NotFound();

            AppUser appUser = await _userManager.FindByEmailAsync(email);

            if (appUser == null)
                return NotFound();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CoffeShop", "shopwisesite@gmail.com"));
            message.To.Add(new MailboxAddress(appUser.UserName, appUser.Email));
            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_env.WebRootPath, "Templates", "Forgetpassword.html")))
            {
                emailbody = stream.ReadToEnd();
            }

            string forgetpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            string url = Url.Action("changepassword", "account", new { id = appUser.Id, token = forgetpasswordtoken }, Request.Scheme);

            emailbody = emailbody.Replace("{{UserName}}", $"{appUser.UserName}").Replace("{{url}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("shopwisesite@gmail.com", "shopwise!@123");
            smtp.Send(message);
            smtp.Disconnect(true);

            return View();
        }

        public async Task<IActionResult> ChangePassword(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            AppUser appUser = await _userManager.FindByIdAsync(Id);

            if (appUser == null)
            {
                return NotFound();
            }

            ResetPasswordVm resetPasswordVM = new ResetPasswordVm
            {
                Id = Id,
                Token = token
            };

            return View(resetPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPasswordVm resetPasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(resetPasswordVM.Id) || string.IsNullOrWhiteSpace(resetPasswordVM.Token))
            {
                return NotFound();
            }

            AppUser appUser = await _userManager.FindByIdAsync(resetPasswordVM.Id);

            if (appUser == null)
            {
                return NotFound();
            }

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(appUser, resetPasswordVM.Token, resetPasswordVM.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(resetPasswordVM);
            }

            return RedirectToAction("Login");
        }
        #region Create Role
        //public async Task<IActionResult> AddRole()
        //{
        //    if (!await _rolemanager.RoleExistsAsync("Admin"))
        //    {
        //        await _rolemanager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    }
        //    if (!await _rolemanager.RoleExistsAsync("Member"))
        //    {
        //        await _rolemanager.CreateAsync(new IdentityRole { Name = "Member" });
        //    }
        //    if (!await _rolemanager.RoleExistsAsync("User"))
        //    {
        //        await _rolemanager.CreateAsync(new IdentityRole { Name = "User" });
        //    }

        //    return Content("Role Yarandi");
        //}
        #endregion
    }
}
