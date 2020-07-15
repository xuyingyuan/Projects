using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreshingStore.API.Helpers;
using FreshingStore.API.Models;
using FreshingStore.Core.Entities;
using FreshingStore.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreshingStore.API.Controllers
{
    [Route("api/products/{productid}/colorcollection")]
    [ApiController]
    public class ProductColorCollectionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductColorService _productColorService;
        private readonly IProductImagesService _productImagesService;
        private readonly IProductService _productService;

        public ProductColorCollectionController(IMapper mapper,
            IProductColorService productColorService,
            IProductImagesService productImageSersvice,
            IProductService productService)
        {
            _mapper = mapper;
            _productColorService = productColorService;
            _productImagesService = productImageSersvice;
            _productService = productService;
        }

        [HttpGet("{colorids}", Name ="GetProductColorCollection")]
        public async Task<ActionResult<IEnumerable<ProductColorDto>>> GetProductColorCollection(int productid, [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> colorids)
        {
            if (colorids == null)
                return BadRequest();
            var productcolors = await _productColorService.GetProductColorsByIdAsync(productid, colorids);
            if(!productcolors.Any())
                return BadRequest();
            var productcolorDtos = _mapper.Map<IEnumerable<ProductColorDto>>(productcolors);
            return Ok(productcolorDtos);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductColor>>> AddProductColors(int productid, IEnumerable<ProductColorForCreationDto> productColorForCreationDtos)
        {
            if (!_productColorService.ExistsProduct(productid))
                return NotFound();
            var productColors = _mapper.Map<IEnumerable<ProductColor>>(productColorForCreationDtos);
            int countAdd = 0;
            foreach (var productcolor in productColors)
            {//only add color is valid and not be created
                productcolor.ProductId = productid;
                if (_productColorService.ExistsColor(productcolor.ColorId)
                    && !_productColorService.ExistsProductColor(productid, productcolor.ColorId))
                {
                    await _productColorService.AddProductColorAsync(productcolor);
                    countAdd++;
                }
            }
            if (countAdd > 0) { 
                _productColorService.Commit();               
            }
            var productcolorDtos = _mapper.Map<IEnumerable<ProductColorDto>>(productColors);
            var idsString = string.Join(",", productcolorDtos.Select(a => a.ColorId.ToString()));
            return CreatedAtRoute("GetProductColorCollection", new { productid, colorids = idsString }, productcolorDtos);
        }

    }
}
