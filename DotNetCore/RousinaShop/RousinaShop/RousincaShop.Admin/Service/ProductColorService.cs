using AutoMapper;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Service
{
    public class ProductColorService: IProductColorService
    {
        private readonly IProductColorRepository _productcolorRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductColorService(IProductColorRepository productColorRepository,
            IProductImageRepository productImageRepository,
            IMapper mapper)
        {
            _productcolorRepository = productColorRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductColorDto> GetProductColorsWithDefaultImages(int productId, int? colorId)
        {
            IEnumerable<ProductColorDto> productColorDtos = null;
            var productcolors = _productcolorRepository.GetProductcolorbyProductId(productId);

            if (productcolors.Count() == 0)
                return null;

            if (colorId == null)
            {
                colorId = productcolors.FirstOrDefault().ColorId;
            }
            var productimages = _productImageRepository.GetProductDefaultImages(productId);
            
            productColorDtos = _mapper.Map<IEnumerable<ProductColorDto>>(productcolors);
            foreach (var c in productColorDtos)
            {
               
                var colorImagesDto = GetProductColorImages(productId, c.ColorId);
                var colorDefaultImageDto = colorImagesDto.FirstOrDefault(i => i.ColorId == c.ColorId);
                c.defaultProductImageDto = colorDefaultImageDto;
                c.productImageDtos = colorImagesDto;
            }

            return productColorDtos;
        }

        public IEnumerable<ProductImageDto> GetProductColorImages(int productId, int colorId)
        {
            var productcolorImages = _productImageRepository.GetProductImagesbyProductIdAndColorId(productId, colorId);
            return (_mapper.Map<IEnumerable<ProductImageDto>>(productcolorImages));
        }

      
    }
}
