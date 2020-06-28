﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var claims = User.Claims.ToList();


            var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            var result = await GetSecret(accessToken);
            return View();
        }

        public async Task<string> GetSecret(string accessToken)
        {
            //retrieve secret data - use accesstoken to access ApiOne method to retrieve data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(accessToken);
            var apiOneUrl = "https://localhost:44331/";
            var response = await apiClient.GetAsync(apiOneUrl + "secret");

            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
