using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Controllers
{
    public class HomeController : Controller
    {
     //   private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
          //  _httpClient = httpClientFactory.CreateClient();
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task< IActionResult> Secret()
        {

            var serverClient = _httpClientFactory.CreateClient();          
            var token = await HttpContext.GetTokenAsync("access_token");
            serverClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var serverurl = "https://localhost:44302/secret/index";      
            var srverResponse = await serverClient.GetAsync(serverurl);
            var refreshTokenResult = await RefreshAccessToken();

            //======after refresh token ===========================
            token = await HttpContext.GetTokenAsync("access_token");
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var apiuri = "https://localhost:44388/secret/index";
            var apiResponse = await apiClient.GetAsync(apiuri);
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

       public async Task<string> RefreshAccessToken()
        {
            
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            var refreshTokenClient = _httpClientFactory.CreateClient();
            
            //basic credential
            var basicCredentials = "UserName:password";
            var encodeCredentials = Encoding.UTF8.GetBytes(basicCredentials);
            var base64Credentials = Convert.ToBase64String(encodeCredentials);

            var requestData = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken
            };

            var reqUrl = "https://localhost:44302/oauth/token";
            var request = new HttpRequestMessage(HttpMethod.Post, reqUrl) {
                Content = new FormUrlEncodedContent(requestData)
            };

            request.Headers.Add("Authorization", $"Basic {base64Credentials}");

            var response = await refreshTokenClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            var newAccessToken = responseData.GetValueOrDefault("access_token");
            var newRefereshToken = responseData.GetValueOrDefault("refresh_token");

            var authInfo = await HttpContext.AuthenticateAsync("SueClientCookie");
            authInfo.Properties.UpdateTokenValue("access_token", newAccessToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", newRefereshToken);

            await HttpContext.SignInAsync("SueClientCookie", authInfo.Principal, authInfo.Properties);

            return "";
        }
    }
}
