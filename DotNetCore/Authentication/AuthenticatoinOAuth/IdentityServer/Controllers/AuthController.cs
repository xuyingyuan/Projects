using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnRul = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            //check if the model is valid

            //sign user
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnRul);
            }
            else if (result.IsLockedOut)
            {

            }
            return View();

        }

       

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnRul = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {

            if (!ModelState.IsValid)
                return View(vm);

            var iUser = new IdentityUser(vm.UserName);
            var createUserResult = await _userManager.CreateAsync(iUser, vm.Password);
            if (!createUserResult.Succeeded)
            {
                return View(vm);
            }

            //sign user
            await _signInManager.SignInAsync(iUser, false);
             return Redirect(vm.ReturnRul);


        }
    }
}
