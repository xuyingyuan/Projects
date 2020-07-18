using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.Models.Models;
using FreshingStore.Core.Entities;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FreshingStore.API.Controllers
{
    [Route("api/products/{productid}/colors")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductColorService _productColorService;
        private readonly IProductImagesService _productImagesService;
        private readonly IProductService _productService;

        public ProductColorsController(IMapper mapper, 
            IProductColorService productColorService,
            IProductImagesService productImageSersvice,
            IProductService productService)
        {
            _mapper = mapper;
            _productColorService = productColorService;
            _productImagesService = productImageSersvice;
            _productService = productService;
        }

        
        [HttpGet]
        [HttpHead]  //HttpHead: for testing Head method
        public async Task<ActionResult<IEnumerable<ProductColorDto>>> GetProductColors(int productid)
        {
            var productcolors = await _productColorService.GetProductColorsByIdAsync(productid, null);
            if (!productcolors.Any())
                return NotFound();

            var productImages = await _productImagesService.GetProductImages(productid, null);
            var productcolorDtos = _mapper.Map<IEnumerable<ProductColorDto>>(productcolors);
            var productImagesDtos = _mapper.Map<IEnumerable<ProductImageDto>>(productImages);
            foreach(var productcolorDto in productcolorDtos)
            { 
                productcolorDto.ProductImageDtos = productImagesDtos.Where(pi => pi.ColorId == productcolorDto.ColorId).ToList();
            }
            return Ok(productcolorDtos);
        }

        // GET api/<ProductColorsController>/5
        [HttpGet("{colorid}", Name ="GetProductcolor")]
        public async Task<ActionResult<ProductColorDto>> GetProductColorById(int productid, int colorid)
        {
            var productcolor = (await _productColorService.GetProductColorsByIdAsync(productid, colorid)).SingleOrDefault();
            if (productcolor == null)
                return NotFound();                    
            var productImages = await _productImagesService.GetProductImages(productid, colorid);
            var productcolorDto = _mapper.Map<ProductColorDto>(productcolor);
            if (productImages.Any())
                productcolorDto.ProductImageDtos = _mapper.Map<IEnumerable<ProductImageDto>>(productImages).ToList(); 

            return Ok(productcolorDto);
        }

      
        [HttpPost]
        public async Task<ActionResult<ProductColorDto>> CreateColorsForProduct(int productid, ProductColorForCreationDto productColorForCreationDto)
        {
            if (!_productService.ExistsProduct(productid))
            {
                return NotFound();
            }

            var productcolor = _mapper.Map<ProductColor>(productColorForCreationDto);
            var colorid = productcolor.ColorId;
            if(_productColorService.ExistsProductColor(productid, colorid))
                return BadRequest("Prodoct and Color already exists");
            productcolor.ProductId = productid;
            await  _productColorService.AddProductColorAsync(productcolor);
            await _productColorService.CommitAsync();
            var productcolorDto = _mapper.Map<ProductColorDto>(productcolor);
            return CreatedAtRoute("GetProductcolor",
                    new { productid = productid, colorid = productcolorDto.ColorId },
                    productcolorDto);
          

        }

        // PUT api/<ProductColorsController>/5
        [HttpPut("{colorid}")]
        public async Task<IActionResult> UpdateProductColor(int productid, int colorid, [FromBody] ProductColorForUpdDto productcolorForUpdDto)
        {
            
            if (!_productService.ExistsProduct(productid))
            {
                return NotFound();
            }

            if(productid <= 0 || colorid <= 0)
                return NotFound();

            //since we already know colorid, we will do upsert: 
            //if productid and colorid not exists in product color, we can add it, if exists, we will do update
            if (!_productColorService.ExistsProductColor(productid, colorid))
            {
                var productcolorForCreate = _mapper.Map<ProductColor>(productcolorForUpdDto);
                productcolorForCreate.ProductId = productid;
                productcolorForCreate.ColorId = colorid;
                await _productColorService.AddProductColorAsync(productcolorForCreate);
                _productColorService.Commit();
                var productcolorDto = _mapper.Map<ProductColorDto>(productcolorForCreate);
                return CreatedAtRoute("GetProductcolor", new { productid, colorid }, productcolorDto);
            }

            var productColorEntity = await _productColorService.GetProductColorByProductAndColorIdAsync(productid, colorid);

            _mapper.Map(productcolorForUpdDto, productColorEntity);                     
             _productColorService.UpdProductColor(productColorEntity);
            await _productColorService.CommitAsync();
            return NoContent();
        }

        [HttpPatch("{colorid}")]
        /*action: Patch
         * content-type: application/json-patch+json
         * sample body: 
         * [{   
                "op": "replace",
                "path": "/colordescription",
                "value": "patch desc repalce"    
            }]
         */
        public async Task<IActionResult> PartiallyUpdateProductColor(int productid, int colorid,
                 JsonPatchDocument<ProductColorForUpdDto> patchDocument)
        {
            if (!_productService.ExistsProduct(productid))
            {
                return NotFound();
            }

            if (productid <= 0 || colorid <= 0)
                return NotFound();

            var productcolor = await _productColorService.GetProductColorByProductAndColorIdAsync(productid, colorid);
            

            if (productcolor == null) // with upsert - we can add new productcolor here
            {
                var productcolorUpdDto = new ProductColorForUpdDto();
                patchDocument.ApplyTo(productcolorUpdDto, ModelState);
                if (!TryValidateModel(productcolorUpdDto))
                {
                    return ValidationProblem(ModelState);
                }
                var productcolorToAdd = _mapper.Map<ProductColor>(productcolorUpdDto);
                productcolorToAdd.ProductId = productid;
                productcolorToAdd.ColorId = colorid;
                await _productColorService.AddProductColorAsync(productcolorToAdd);
                await _productColorService.CommitAsync();
                var productcolorDto = _mapper.Map<ProductColorDto>(productcolorToAdd);
                return CreatedAtRoute("GetProductcolor", new { productid, colorid }, productcolorDto);              
            }

            var productcolorToPatch = _mapper.Map<ProductColorForUpdDto>(productcolor);

            patchDocument.ApplyTo(productcolorToPatch, ModelState);           
            //after apply pathdocument, we need to validate ojbect
            if(!TryValidateModel(productcolorToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(productcolorToPatch, productcolor);
            _productColorService.UpdProductColor(productcolor);
            await _productColorService.CommitAsync();
            return NoContent();

        }

        // DELETE api/<ProductColorsController>/5
        [HttpDelete("{colorid}")]
        public async Task<IActionResult> DeleteProductcolor(int productid, int colorid)
        {
            if(!_productColorService.ExistsProductColor(productid, colorid))
            {
                return NotFound();
            }

            if(_productColorService.ExistsSku(productid, colorid))
            {
                return BadRequest("This product color will not be deleted, since SKUs already exists.");
            }
            var ProductColor = await _productColorService.GetProductColorByProductAndColorIdAsync(productid, colorid);
            _productColorService.RemoveProductColor(ProductColor);
            await _productColorService.CommitAsync();

            return NoContent();
        }

        //override validationProblem method to have more detailed error output.
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary )
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        [HttpOptions]
        public IActionResult GetProductColorOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,PATCH,OPTIONS");
            return Ok();
        }
    }
}
