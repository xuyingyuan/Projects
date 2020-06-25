using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Server.Constants;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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


       
  
        public IActionResult authenticate()
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_userId"), //user id
                new Claim("granny", "cookie")
            };
            var secretByte = Encoding.UTF8.GetBytes(JwtTokenConstants.Secret);
            var key = new SymmetricSecurityKey(secretByte);
            var algrithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key, algrithm);

            var token = new JwtSecurityToken(
                JwtTokenConstants.Issuer,
                JwtTokenConstants.Audiance,
                Claims,
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddMinutes(10),
                signingCredentials
                ); //c# way to representing token

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { access_token = tokenJson}); //check token on jwt.io

        }

        public IActionResult Decode(string part)
        {
            var bytes = Convert.FromBase64String(part);


            return Ok(Encoding.UTF8.GetString(bytes));
        }

       
    }
}
