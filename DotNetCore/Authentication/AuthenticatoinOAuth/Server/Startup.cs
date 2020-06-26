using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Constants;

namespace Server
{
    public class Startup
    {
        //JWT - Jason web Token
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("OAuth").AddJwtBearer("OAuth", config => {
                var secretByte = Encoding.UTF8.GetBytes(JwtTokenConstants.Secret);
                var key = new SymmetricSecurityKey(secretByte);

                //check if token passed from Querystring
                config.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.ContainsKey("access_token"))
                        {
                            context.Token = context.Request.Query["access_token"];
                        }

                        return Task.CompletedTask;
                    }
                };

                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ClockSkew = TimeSpan.Zero,      //override default expired time as 5 min.
                    ValidIssuer = JwtTokenConstants.Issuer,
                    ValidAudience = JwtTokenConstants.Audiance,
                    IssuerSigningKey = key
                };
            });



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
