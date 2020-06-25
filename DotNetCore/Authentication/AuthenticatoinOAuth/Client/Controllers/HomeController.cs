using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

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
        public async Task< IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return View();
        }


       
  
        public IActionResult authenticate()
        {
          

            return Ok(); //check token on jwt.io

        }

        public IActionResult Decode(string part)
        {
            var bytes = Convert.FromBase64String(part);


            return Ok(Encoding.UTF8.GetString(bytes));
        }

       
    }
}
