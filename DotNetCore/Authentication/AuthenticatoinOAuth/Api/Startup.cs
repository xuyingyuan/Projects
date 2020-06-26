using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.JwtRequirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("DefaultAuth").AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("DefaultAuth", null);
                
            

            services.AddAuthorization(config=> {

                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthPolicy = defaultAuthBuilder 
                    .AddRequirements(new JwtRequirement())
                    .Build();
                config.DefaultPolicy = defaultAuthPolicy;
            });

            services.AddScoped<IAuthorizationHandler, JwtReuirementHandler>();

            services.AddHttpClient().AddHttpContextAccessor();
            services.AddControllers();

        }

       
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
