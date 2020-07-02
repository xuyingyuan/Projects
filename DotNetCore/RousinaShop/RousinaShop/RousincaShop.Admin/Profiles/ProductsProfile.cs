using RousincaShop.Admin.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RousincaShop.Admin.Models;
using RousincaShop.Admin.Data.Repositories;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;

namespace RousincaShop.Admin.Profiles
{
    public class ProductsProfile : Profile
    {
      
        public ProductsProfile()
        {
            
            mappingEntityToDto();

         
          
        }

        private void mappingEntityToDto()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDetailDto>();

            CreateMap<ProductImage, ProductImageDto>()
                .ForMember(dest=>dest.ImageTypeDescription, opt=>
                    opt.MapFrom(src =>src.ImageTypeNavigation.TypeDescription));

            CreateMap<Sku, SkuDto>().ForMember(dest => dest.SizeName, opt =>
                    opt.MapFrom(src => src.SizeCodeNavigation.Name)); ;
            CreateMap<CategoryItem, CategoryItemDto>();
            CreateMap<SizeCode, SizeCodeDto>();
            CreateMap<ProductColor, ProductColorDto>();
        }

        private void mappingDtoToEntity()
        {
            CreateMap<ProductDto, Product>();

        }
    }
}
