using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Constraints
{
    public class ActionVersionConstraint : IActionConstraint
    {
        private double _requiredVersion;
        public ActionVersionConstraint(double version)
        {
            _requiredVersion = version;
        }
        public int Order { get; set; }

        public bool Accept(ActionConstraintContext context)
        {
            double reqVersion;
           
            if (double.TryParse(context.RouteContext.HttpContext.Request.Headers["x-version"].ToString(), out reqVersion))
            {
                return reqVersion >= _requiredVersion && reqVersion < _requiredVersion + 1;
            }
            return false;
        } 
    }
}
