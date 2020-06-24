using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;

        public HomeController(UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signinManager = signInManager;
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
                var signInResult = await _signinManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index");

        }
    }
}
