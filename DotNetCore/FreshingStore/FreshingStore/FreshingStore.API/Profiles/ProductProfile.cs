using AutoMapper;
using FreshingStore.API.Models;
using FreshingStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshingStore.API.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            MappingEntityToDto();
            MappingDtoToEntity();
        }

        private void MappingEntityToDto()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductColor, ProductColorDto>();
            CreateMap<ProductImage, ProductImageDto>();
        }

        private void MappingDtoToEntity()
        {
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductColorForCreationDto, ProductColor>();

            CreateMap<ProductIColorImageForCreationDto, ProductImage>();
            
        }
    }
}
