using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MvcClient
{
    public class Startup
    {
        //openid doc url: https://openid.net/specs/openid-connect-core-1_0.html#Authentication
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config=> {
                config.DefaultScheme = "SueMVCCookie";
                config.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("SueMVCCookie")
                .AddOpenIdConnect("oidc", config=> {
                    config.Authority = "https://localhost:44337/";    //sever url
                    config.ClientId = "client_id_mvc";
                    config.ClientSecret = "client_secret_mvc";
                    config.SaveTokens = true;

                    config.ResponseType = "code";

                    //configure cookie claim mapping, or delete some claims
                    config.ClaimActions.DeleteClaim("mar");
                    config.ClaimActions.DeleteClaim("s_hash");
                    config.ClaimActions.MapUniqueJsonKey("mappedSueClaimOne", "sue.claimOne");


                    //tow trips to load claims into the cookie, so keep the id token smaller
                    config.GetClaimsFromUserInfoEndpoint = true;        //get user claim

                    //config scope
                    config.Scope.Clear();
                    config.Scope.Add("openid");
                    config.Scope.Add("SueClaim.scope");
                    config.Scope.Add("ApiOne");
                    config.Scope.Add("ApiTwo");
                   
                });

            services.AddHttpClient();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
