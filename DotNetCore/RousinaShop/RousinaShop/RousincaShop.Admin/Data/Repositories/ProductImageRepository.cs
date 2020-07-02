using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Data.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(RousinaDBContext dbcontext) : base(dbcontext) { }



        public IEnumerable<ProductImage> GetProductImagesbyProductIdAndColorId(int productId, int colorId)
        {
            return _dbContext.ProductImages.Where(pi => pi.ProductId == productId && pi.ColorId == colorId);
        }

        public IEnumerable<ProductImage> GetProductDefaultImages(int productId)
        {
            return _dbContext.ProductImages.Where(pi => pi.ProductId == productId && pi.ImageType == "FRN");
        }

        public ProductImage GetProductColorDefaultImages(int productId, int colorId)
        {
            return _dbContext.ProductImages.Where(pi => pi.ProductId == productId && pi.ImageType == "FRN" && pi.ColorId == colorId).FirstOrDefault();
        }
    }
}
