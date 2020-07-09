using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.API.Models;
using FreshingStore.Core.Entities;
using FreshingStore.Logger.Logging;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreshingStore.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ILoggerManager logger, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (products.Count() == 0)
                return NotFound();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound();
           
            return Ok(_mapper.Map<ProductDto>(product));
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task Post([FromBody] Product product)
        {
           await _productService.AddProductAsync(product);
            _productService.Commit();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            _productService.UpdProduct(product);
            _productService.Commit();
        }



        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var product = await _productService.GetProductAsync(id);
             _productService.RemoveProduct(product);
            _productService.Commit();
        }

        public IActionResult PartialUpdateProduct(int id, JsonPatchDocument<ProductDto> patchProduct)
        {
            var patchProductDto = new ProductDto();
            patchProduct.ApplyTo(patchProductDto, ModelState);

            var productToUpd = _mapper.Map<Product>(patchProductDto);
            productToUpd.Id = id;
            _productService.UpdProduct(productToUpd);
            _productService.Commit();
            return CreatedAtRoute("GetProductById", new { id});
        }
    }
}
