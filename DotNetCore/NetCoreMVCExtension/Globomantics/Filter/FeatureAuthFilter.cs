using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Filter
{
    public class FeatureAuthFilter : Attribute, IAuthorizationFilter
    {
        public string FeatureName { get; set; }
        private Dictionary<string, bool> FeatureStatus = new Dictionary<string, bool>
        {
            {"Loan", false },
            {"Insureance", true},
            {"Resources", true }
        };

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!FeatureStatus[FeatureName])
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
