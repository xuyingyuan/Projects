using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Basic.CustomAttributes;
using Basic.CustomPolicyProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
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


        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View();
        }

        [Authorize(Roles =  "Admin")]
        public IActionResult AdminRole()
        {
            return View();
        }

        [SecurityLevel(7)]
        public IActionResult SecurityLevel()
        {
            return View();
        }

        [SecurityLevel(10)]
        public IActionResult SecurityHighLevel()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult authenticate()
        {
            var gradmaClaims = new List<Claim>() {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "xyz@email.com"),
                 new Claim(ClaimTypes.DateOfBirth, "01/01/1989"),
                 new Claim(ClaimTypes.Role, "Admin"),
                 new Claim(DynamicPolicies.SecurityLevel, "7"),
                new Claim("Grandma.Says", "very nice")
            };

            var licenseClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bob K"),
                new Claim(ClaimTypes.Email, "xyz@test.com"),               
                new Claim("DriverLicense", "A")
            };


            var grandmaIdentity = new ClaimsIdentity(gradmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrinciple = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

            HttpContext.SignInAsync(userPrinciple);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DoStuff()
        {
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customerPolicy = builder.RequireClaim("hello").Build();
            // var result = await _authorizationService.AuthorizeAsync(User, "Claim.DoB");
            var result = await _authorizationService.AuthorizeAsync(User, customerPolicy);
            if(result.Succeeded)
                return View();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DoStuff2([FromServices]IAuthorizationService authorizationService)
        {
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customerPolicy = builder.RequireClaim("hello2").Build();
            // var result = await _authorizationService.AuthorizeAsync(User, "Claim.DoB");
            var result = await authorizationService.AuthorizeAsync(User, customerPolicy);
            if (result.Succeeded)
                return View();

            return RedirectToAction("Index");
        }
    }
}
