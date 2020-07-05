using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreshingStore.Core.Entities;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;


namespace FreshingStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productService.GetProductsAsyn();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await _productService.GetProductAsyn(id);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task Post([FromBody] Product product)
        {
           await _productService.AddProductAsyn(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public  void Delete(int id)
        {
            var product = _productService.GetProduct(id);
             _productService.RemoveProduct(product);
        }
    }
}
