using Basic.CustomPolicyProvider;
using Microsoft.AspNetCore.Authorization;

namespace Basic.CustomAttributes
{
    public class SecurityLevelAttribute : AuthorizeAttribute
    {
        public SecurityLevelAttribute(int level)
        {
            Policy = $"{DynamicPolicies.SecurityLevel}.{level}";
        }
    }
}
