using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.API.Helpers;
using FreshingStore.API.Models;
using FreshingStore.API.Models.Product;
using FreshingStore.Core.Entities;
using FreshingStore.Logger.Logging;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshingStore.API.Controllers
{
    
    [ApiController]
    [Route("api/productcollection")]
    public class ProductCollectionController : ControllerBase
    {
        private IProductService _productService;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductCollectionController(IProductService productService, ILoggerManager logger, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        //array key: 1,2,3
        //composite key: key1=value1,key2=value3..
        [HttpGet("({ids})", Name ="GetAuthorCollection")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductCollections(
                [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            if (ids == null)
                return BadRequest();

            var products = await _productService.GetProductsAsync(ids);
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProductCollection(
            IEnumerable<ProductForCreationDto> productcreationDtos)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productcreationDtos);
            foreach(var product in products)
            {
                await _productService.AddProductAsync(product);
            }
            await _productService.CommitAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            var idsString = string.Join(",", productDtos.Select(a => a.Id.ToString()));
            return CreatedAtRoute("GetAuthorCollection", new {ids= idsString }, productDtos);
        }
    }
}
