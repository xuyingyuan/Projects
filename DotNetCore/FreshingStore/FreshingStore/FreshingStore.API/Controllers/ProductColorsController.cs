using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.API.Models;
using FreshingStore.Core.Entities;
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
        public async Task<ActionResult<ProductColorDto>> CreateColorsForProduct(int productid, ProductColorForCreationDto productColorCreationDto)
        {
            if (!_productService.ProductExist(productid))
            {
                return NotFound();
            }

            var productcolor = _mapper.Map<ProductColor>(productColorCreationDto);
            productcolor.ProductId = productid;
            var added = await _productColorService.AddProductColorAsync(productcolor);
            if (!added)
                return BadRequest("Prodoct and Color already exists");

            _productColorService.Commit();
            var productcolorDto = _mapper.Map<ProductColorDto>(productcolor);
            return CreatedAtRoute("GetProductcolor",
                    new { productid = productid, colorid = productcolorDto.ColorId },
                    productcolorDto);
          

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
