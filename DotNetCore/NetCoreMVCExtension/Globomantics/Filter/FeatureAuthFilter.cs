using Globomantics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Filter
{
    public class FeatureAuthFilter :  IAuthorizationFilter
    {
        private IFeatureService _featureService;
        private string _featureName;

        //public string FeatureName { get; set; }
        //private Dictionary<string, bool> FeatureStatus = new Dictionary<string, bool>
        //{
        //    {"Loan", false },
        //    {"Insureance", true},
        //    {"Resources", true }
        //};

        public FeatureAuthFilter(IFeatureService featureService, string featureName)
        {
            this._featureService = featureService;
            this._featureName = featureName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
           // if (!FeatureStatus[FeatureName])
           if(_featureService.IsFeatureActive(_featureName))
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
