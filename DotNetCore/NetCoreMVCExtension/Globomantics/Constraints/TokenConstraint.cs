using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globomantics.Constraints
{
    public class TokenConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return IsTokenValid(values["token"].ToString());
        }

        private bool IsTokenValid(string toKen)
        {
            int LetterCount = 0;
            int numCount = 0;
            double numSum = 0;
            foreach(var unit in toKen)
            {
                if (char.IsLetter(unit))
                {
                    LetterCount += 1;
                }else if (char.IsDigit(unit))
                {
                    numCount += 1;
                    numSum += unit;
                }
            }
            return LetterCount == 3 && numCount == 3 && numSum % 2 == 0;
        }
    }
}
