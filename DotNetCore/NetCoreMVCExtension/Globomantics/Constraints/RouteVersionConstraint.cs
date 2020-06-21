using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Constraints
{
    public class RouteVersionConstraint : IRouteConstraint
    {
        private double _requiredVersion; 
        public RouteVersionConstraint(double version)
        {
            _requiredVersion = version;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            double reqVersion;
            var urlVersion = values["version"].ToString()?.Substring(1);
            if(double.TryParse(urlVersion, out reqVersion))
            {
                return reqVersion >= _requiredVersion && reqVersion < _requiredVersion + 1;
            }
            return false;
        }
    }
}
