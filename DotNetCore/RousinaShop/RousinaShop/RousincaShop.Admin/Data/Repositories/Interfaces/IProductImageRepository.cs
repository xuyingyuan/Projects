using RousincaShop.Admin.Data.Entities;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Repositories.Interfaces
{
    public  interface IProductImageRepository : IGenericRepository<ProductImage>
    {

        IEnumerable<ProductImage> GetProductImagesbyProductIdAndColorId(int productId, int colorId);

        IEnumerable<ProductImage> GetProductDefaultImages(int productId);

        ProductImage GetProductColorDefaultImages(int productId, int colorId);
    }
}
