﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Globomantics.Models;
using Globomantics.Core.Models;

namespace Globomantics.Controllers
{
    public class ServicesController : Controller
    {

        public IActionResult Submission(List<Submission> submissions)
        {
            // TODO: Save submissions
            return View();
        }


    }
}
