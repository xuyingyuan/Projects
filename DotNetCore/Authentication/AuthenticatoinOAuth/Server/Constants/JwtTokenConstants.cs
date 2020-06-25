using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Constants
{
    public static class JwtTokenConstants
    {
       
        public const string Audiance = "https://localhost:44302/";
        public const string Issuer = Audiance;
        public const string Secret = "not_too_short_secret_or_it_give_you_error";
    }
}
