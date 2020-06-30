using RousincaShop.Admin.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RousincaShop.Admin.Models;

namespace RousincaShop.Admin.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, Product>();
        }
    }
}
