using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FreshingStore.API.Controllers
{
    [ApiController]
    [Route("api/products/{id}/colors")]
    public class ColorsController : ControllerBase
    {
        private IMapper _mapper;

        public ColorsController(IMapper mapper)
        {
            _mapper = mapper;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
