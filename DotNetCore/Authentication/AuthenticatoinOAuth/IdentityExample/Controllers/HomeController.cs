using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly IEmailService _emailService;

        public HomeController(UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }


        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //login functonality
            var user = await _userManager.FindByNameAsync(username);
            if(user != null)
            {
                //sign in
               var signInResult =   await  _signinManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            //register functonality
            var user = new IdentityUser {
                UserName = username,
               Email = ""
            };
           var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {

                //var signInResult = await _signinManager.PasswordSignInAsync(user, password, false, false);
                //if (signInResult.Succeeded)
                //{
                //    return RedirectToAction("Index");
                //}

                //generate email token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //_userManager.ConfirmEmailAsync
                var link = Url.Action(nameof(VerifyEmail), "Home", new {userId=user.Id, code }, Request.Scheme, Request.Host.ToString());

                //  await _emailService.SendAsync("testo@test.com", "verify code", "this is verify code" + $"<a href=\"{link}\">verify email</a>", true);
                TempData["linkText"] = link;
                return RedirectToAction("EmailVerification");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest();
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return View();
            return BadRequest();
        }

        public IActionResult EmailVerification() => View();

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index");

        }
    }
}
