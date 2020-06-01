﻿using IdentityModel;
using ImageGallery.Client.HttpHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ImageGallery.Client
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                 .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            //add policy:
            services.AddAuthorization(authorizations =>
            {
                authorizations.AddPolicy("CanOrderFrame", policybuilder =>
                {
                    policybuilder.RequireAuthenticatedUser();
                    policybuilder.RequireClaim("country", "be", "cn", "ca");
                    policybuilder.RequireClaim("subscriptionlevel", "PayingUser");
                });
            });

            services.AddHttpContextAccessor();
            services.AddTransient<BearerTokenHandler>();

            // create an HttpClient used for accessing the API
            services.AddHttpClient("APIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44366/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>();

            

            //create HttpClient used to accessing the IDP
            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

            services.AddAuthentication(options =>
             {
                 options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
             }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options=> {
                 options.AccessDeniedPath = "/Authorization/AccessDenied";
             })
          .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
          {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Authority = "https://localhost:5001";        // IDP
            options.ClientId = "ImageGallaryClient1";
            options.ResponseType = "code";
            //options.UsePkce = true;      //use pkce protection
            //// options.CallbackPath = new PathString("...");
            //options.Scope.Add("openid");
            //options.Scope.Add("profile");
            options.Scope.Add("address");
            options.Scope.Add("roles");
            options.Scope.Add("imagegalleryapi");
              options.Scope.Add("country");
              options.Scope.Add("subscriptionlevel");
              options.Scope.Add("offline_access");
            //for claim: 
            //options.ClaimActions.Remove("nbf");
            //options.ClaimActions.DeleteClaim("address");
            options.ClaimActions.DeleteClaim("sid");
            options.ClaimActions.DeleteClaim("idp");
            options.ClaimActions.DeleteClaim("s_hash");
            options.ClaimActions.DeleteClaim("auth_time");
            options.ClaimActions.MapUniqueJsonKey("role", "role");
              options.ClaimActions.MapUniqueJsonKey("country", "country");
              options.ClaimActions.MapUniqueJsonKey("subscriptionlevel", "subscriptionlevel");

              options.SaveTokens = true;
            options.ClientSecret = "secret";
            options.GetClaimsFromUserInfoEndpoint = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  NameClaimType = JwtClaimTypes.GivenName,
                  RoleClaimType = JwtClaimTypes.Role
              };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Gallery}/{action=Index}/{id?}");
            });
        }
    }
}
