using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.API.Models;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshingStore.API.Controllers
{
    [Route("api/products/{productid}/colors")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductColorService _productColorService;

        public ProductColorsController(IMapper mapper, 
            IProductColorService productColorService)
        {
            _mapper = mapper;
            _productColorService = productColorService;
        }

        // GET: api/<ProductColorsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColorDto>>> GetProductColors(int productid)
        {
            var productcolor = await _productColorService.GetProductColorsByProductIdAsync(productid);
            var productcolorDtos = _mapper.Map< IEnumerable<ProductColorDto>>(productcolor);
            return Ok(productcolorDtos);
        }

        // GET api/<ProductColorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductColorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductColorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductColorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
