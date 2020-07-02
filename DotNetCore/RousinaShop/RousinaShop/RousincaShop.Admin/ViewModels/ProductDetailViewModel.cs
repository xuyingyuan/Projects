using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.ViewModels
{
    public class ProductDetailViewModel
    {
        ProductDto productDto { get; set; }
        ProductImageDto productImageDto { get; set; }
        SkuDto skuDto { get; set; }
    }
}
