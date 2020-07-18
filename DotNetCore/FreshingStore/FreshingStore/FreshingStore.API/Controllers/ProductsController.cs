using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.Core.Entities;
using FreshingStore.Logger.Logging;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using FreshingStore.Service.ResourceParameters;
using FreshingStore.Models.Models.Product;

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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] ProductResourceParameters productResourcParameters)
        {
            var products = await _productService.GetProductsAsync(productResourcParameters);
            if (products.Count() == 0)
                return NotFound();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        // GET api/products/5
        [HttpGet("{id}", Name ="GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            if (id <= 0)
                return BadRequest();
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound();
           
            return Ok(_mapper.Map<ProductDto>(product));
        }

        // POST api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductForCreationDto productForCreationDto)
        {
            if (productForCreationDto is null)
                return BadRequest();
            var product = _mapper.Map<Product>(productForCreationDto);
           await _productService.AddProductAsync(product);
            _productService.Commit();
            var productDto = _mapper.Map<ProductDto>(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, productDto);
        }

        
        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductForUpdDto productForUpdDto)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound();

            _mapper.Map(productForUpdDto, product);
            _productService.UpdProduct(product);
            _productService.Commit();
            return NoContent();
        }



        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_productService.ExistsSku(id))
                return BadRequest("You can't delete this product since SKUs already exists.");

            var product = await _productService.GetProductAsync(id);
            _productService.RemoveProduct(product);
            _productService.Commit();
            return NoContent();
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

        [HttpOptions]
        public IActionResult GetProductsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,OPTIONS");
            return Ok();
        }
    }
}
