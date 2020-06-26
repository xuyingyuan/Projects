using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server.Constants;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
                string response_type,   //authorization flow type
                string client_id,   //client id
                string redirect_uri,    //redirect uri ( client?)
                string scope,       //what information want: email, mycookie, 
                string state)       //random string generated to confirm that we are going to gack to the same client
        {

            var query = new QueryBuilder();
            query.Add("reidrectUrl", redirect_uri);
            query.Add("state", state);
            return View(model:query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(string username, string password,
              string reidrectUrl,               
                string state)
        {
            const string code = "string code abcdc";
            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            return Redirect($"{reidrectUrl}{query.ToString()}");
        }

        public async Task< IActionResult> Token(
            string grant_type,
            string code,    //comfirmaiton of the authentication
            string redirect_uri,
            string client_id )
        {
            var Claims = new[]
          {
                new Claim(JwtRegisteredClaimNames.Sub, "some_userId"), //user id
                
                new Claim(JwtRegisteredClaimNames.Birthdate, "11/24/1985"),
               
            };
            var secretByte = Encoding.UTF8.GetBytes(JwtTokenConstants.Secret);
            var key = new SymmetricSecurityKey(secretByte);
            var algrithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key, algrithm);

            var token = new JwtSecurityToken(
                JwtTokenConstants.Issuer,
                JwtTokenConstants.Audiance,
                Claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials
                ); //c# way to representing token

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);
            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "auathTest"
            };

            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseByte = Encoding.UTF8.GetBytes(responseJson);
            await Response.Body.WriteAsync(responseByte, 0, responseByte.Length);
            return Redirect(redirect_uri);

        }
        [Authorize]
        public IActionResult Validate()
        {
            if(HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {
                var token = accessToken;
                return Ok();
            }
            return BadRequest();
        }
    }
}
