﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreshingStore.Core.Entities;
using FreshingStore.Logger.Logging;
using FreshingStore.Service.Interface;
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

        public ProductsController(IProductService productService, ILoggerManager logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
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
        public  void Delete(int id)
        {
            var product = _productService.GetProduct(id);
             _productService.RemoveProduct(product);
            _productService.Commit();
        }
    }
}
