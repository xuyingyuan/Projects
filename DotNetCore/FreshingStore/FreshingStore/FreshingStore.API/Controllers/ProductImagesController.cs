using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.API.Models;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshingStore.API.Controllers
{
    [Route("api/{productid}/ProductImages")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductColorService _productColorService;
        private readonly IProductImagesService _productImagesService;
        private readonly IProductService _productService;

        public ProductImagesController(IMapper mapper,
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
        public async Task<ActionResult<IEnumerable<ProductImageDto>>> GetProductImages(int productid)
        {
            var productImages = await _productImagesService.GetProductImages(productid, null);
            if (!productImages.Any())
                return NotFound();
            var productImageDtos = _mapper.Map<ProductImageDto>(productImages);
            return Ok(productImageDtos);

        }
    }
}
