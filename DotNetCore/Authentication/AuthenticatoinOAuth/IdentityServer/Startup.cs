using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Data;
using IdentityServer.Statics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //check this when start runing: https://localhost:44337/.well-known/openid-configuration
        //not works for identityServer4 4.0.0 - accessToken is null -- why????
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDBContext>(config => {
                config.UseInMemoryDatabase("memory");
            });

            //AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                //config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();    //token in url


            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.cookie";
                config.LoginPath = "/Auth/Login";
            });


            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(Configuration.GetApis())
                .AddInMemoryIdentityResources(Configuration.GetIdentityResource())
                .AddInMemoryClients(Configuration.GetClients())
                .AddDeveloperSigningCredential();   //generate developer certificate key: tempkey.jwk




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

            app.UseIdentityServer();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
