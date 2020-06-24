using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Basic.CustomPolicyProvider
{
 
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

             
        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            foreach(var customerPolicy in DynamicPolicies.Get())
            {
                if (policyName.StartsWith(customerPolicy))
                {
                    var policy = DynamicAuthorzationPolicyFactory.Create(policyName);
                    return Task.FromResult(policy);
                }
            }
            return base.GetPolicyAsync(policyName);
        }
    }

   
    public static class DynamicPolicies
    {
        public static IEnumerable<string> Get()
        {
            yield return SecurityLevel;
            yield return Rank;
        }
        public const string SecurityLevel = "SecurityLevel";
        public const string Rank = "Rank";

    }

    public static class DynamicAuthorzationPolicyFactory
    {
        public static AuthorizationPolicy Create(string policyName)
        {
            var parts = policyName.Split(".");
            var type = parts.First();
            var value = parts.Last();
            switch (type)
            {
                case DynamicPolicies.Rank:
                    return new AuthorizationPolicyBuilder()
                        .RequireClaim("Rank", value)
                        .Build();
                case DynamicPolicies.SecurityLevel:
                    return new AuthorizationPolicyBuilder()
                     .AddRequirements(new SecuritylevelRequirement(Convert.ToInt32(value)))
                     .Build();
                default:
                    return null;
            }
        }
    }

    public class SecuritylevelRequirement: IAuthorizationRequirement
    {
        public int Level { get; }
        public SecuritylevelRequirement(int level)
        {
            Level = level;
        }
    }

    public class SecuritylevelHandler : AuthorizationHandler<SecuritylevelRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            SecuritylevelRequirement requirement)
        {
            var claimValue = Convert.ToInt32(context.User.Claims
                .FirstOrDefault(x => x.Type == DynamicPolicies.SecurityLevel)
                ?.Value??"0");

            if(requirement.Level <= claimValue)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
