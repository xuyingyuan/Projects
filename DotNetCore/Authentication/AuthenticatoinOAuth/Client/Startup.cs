using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Client
{
    public class Startup
    {
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config=> {
                //check cookie to confirm that this is are authenticated
                config.DefaultAuthenticateScheme = "SueClientCookie";
                //when sign, we will deal with cookie
                config.DefaultSignInScheme = "SueClientCookie";
                //use this to check if we are allowed to do something
                config.DefaultChallengeScheme = "OurServer";
                })
                .AddCookie("SueClientCookie")
                .AddOAuth("OurServer", config=> {
                    config.ClientId = "client_id";
                    config.ClientSecret = "client_secreat";
                    config.CallbackPath = "/oauth/callback";
                    config.AuthorizationEndpoint = "https://localhost:44302/oauth/authorize";
                    config.TokenEndpoint = "https://localhost:44302/oauth/token";
                    config.SaveTokens = true;

                    config.Events = new OAuthEvents()
                    {
                        OnCreatingTicket = context =>
                        {
                            var accessToken = context.AccessToken;
                            var base64playload = accessToken.Split('.')[1];
                            var base64Check = base64playload.Length % 4;
                            if (base64Check != 0)                           
                                base64playload = base64playload + ("====").Substring(base64Check);
                            var bytes = Convert.FromBase64String(base64playload);
                            var jsonPlayload = Encoding.UTF8.GetString(bytes);
                            var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPlayload);
                            //IEnumerable<Claim> claims = context.identity.Claims;
                            foreach (var claim in claims)
                            {
                                context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddHttpClient();

            services.AddControllersWithViews()
                 .AddRazorRuntimeCompilation(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();



            //who are you?
            app.UseAuthentication();

            //are you allowed?
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
