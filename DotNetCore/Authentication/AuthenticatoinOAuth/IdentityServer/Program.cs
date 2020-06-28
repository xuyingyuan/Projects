using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
                
                using(var scope = host.Services.CreateScope())
            {
                var userManage = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                var user = new IdentityUser("sue");
                userManage.CreateAsync(user, "password").GetAwaiter().GetResult();
                //add claims: 
                userManage.AddClaimAsync(user, new Claim("sue.claimOne", "small cat.cookie")).GetAwaiter().GetResult();
                userManage.AddClaimAsync(user, new Claim("sue.api.claimOne", "big cat.cookie1")).GetAwaiter().GetResult();
                userManage.AddClaimAsync(user, new Claim("sue.api.claimTwo", "big cat.cookie2")).GetAwaiter().GetResult();

            }

                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
