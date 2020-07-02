using RousincaShop.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Service
{
    public interface IProductColorService
    {
        IEnumerable<ProductColorDto> GetProductColorsWithDefaultImages(int productId, int? colorId);

        IEnumerable<ProductImageDto> GetProductColorImages(int productId, int colorId);
    }
}
