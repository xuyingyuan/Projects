using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task< IActionResult> Secret()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var serverurl = "https://localhost:44302/secret/index";
            var apiuri = "https://localhost:44388/secret/index";

            var srverResponse = await _httpClient.GetAsync(serverurl);
            var apiResponse = await _httpClient.GetAsync(apiuri);
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
