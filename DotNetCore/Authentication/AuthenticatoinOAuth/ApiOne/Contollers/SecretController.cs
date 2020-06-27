using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOne.Contollers
{
    public class SecretController: Controller
    {
        [Authorize]
        [Route("/Secret")]
        public string Index()
        {
            return "secret message from ApiOne";
        }
    }
}
