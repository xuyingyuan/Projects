using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Models;
using RousincaShop.Admin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.ViewComponents
{
    public class ProductColorsViewComponent : ViewComponent
    {
        private readonly IProductColorService _productcolorService;

        public ProductColorsViewComponent(IProductColorService productColorService)
        {
            _productcolorService = productColorService;
        
        }

        public IViewComponentResult Invoke(int productId, int? colorId)
        { 
            var productColorDtos = _productcolorService.GetProductColorsWithDefaultImages(productId, colorId);

            return View(productColorDtos);
        }

       

    }
}
