using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basic.AuthorizationRequirements
{
    public class CookieJarAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, CookieJar>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            CookieJar cookieJar)
        {
            if (requirement.Name == CookieJarOperations.Look)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement.Name == CookieJarOperations.ComNear)
            {
                if (context.User.HasClaim("Friend", "Good"))
                {
                    context.Succeed(requirement);
                }
            }
            else if(requirement.Name == CookieJarOperations.Open)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public static class CookieJarOperations
    {
        public static string Open = "open";
        public static string TakeCookie = "takecookie";
        public static string ComNear = "comenear";
        public static string Look = "look";
    }

    public class CookieJar
    {
        public string Name { get; set; }
    }
}
