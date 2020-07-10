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
        private readonly IProductImagesService _productImagesService;

        public ProductColorsController(IMapper mapper, 
            IProductColorService productColorService,
            IProductImagesService productImageSersvice)
        {
            _mapper = mapper;
            _productColorService = productColorService;
            _productImagesService = productImageSersvice;
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
        [HttpGet("{colorid}")]
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
