using AutoMapper;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Data.Repositories.Wrapper;
using RousincaShop.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Service
{
    public class ProductColorService: IProductColorService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;      
        private readonly IMapper _mapper;

        public ProductColorService(IRepositoryWrapper repositoryWrapper,            
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;          
            _mapper = mapper;
        }

        public IEnumerable<ProductColorDto> GetProductColorsWithDefaultImages(int productId, int? colorId)
        {
            IEnumerable<ProductColorDto> productColorDtos = null;
            var productcolors = _repositoryWrapper.ProductColor.GetProductcolorbyProductId(productId);

            if (productcolors.Count() == 0)
                return null;

            if (colorId == null)
            {
                colorId = productcolors.FirstOrDefault().ColorId;
            }
            var productimages = _repositoryWrapper.ProductImage.GetProductDefaultImages(productId);
            
            productColorDtos = _mapper.Map<IEnumerable<ProductColorDto>>(productcolors);
            foreach (var c in productColorDtos)
            {
               
                var colorImagesDto = GetProductColorImages(productId, c.ColorId);
                var colorDefaultImageDto = colorImagesDto.FirstOrDefault(i => i.ColorId == c.ColorId);
                c.defaultProductImageDto = colorDefaultImageDto;
                c.productImageDtos = colorImagesDto.ToList();
            }

            return productColorDtos;
        }

        public IEnumerable<ProductImageDto> GetProductColorImages(int productId, int colorId)
        {
            var productcolorImages = _repositoryWrapper.ProductImage.GetProductImagesbyProductIdAndColorId(productId, colorId);
            return (_mapper.Map<IEnumerable<ProductImageDto>>(productcolorImages));
        }

      
    }
}
