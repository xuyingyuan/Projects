using FreshingStore.Core.Entities;
using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository;
using FreshingStore.Repo.Repository.Interfaces;
using FreshingStore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Services
{
    public class ProductColorService: BaseService, IProductColorService
    {

        public ProductColorService(AppDBContext appDBContext):base(appDBContext)
        {
           
        }

        public async Task<bool> AddProductColorAsync(ProductColor productColor)
        {
           if(ExistsProductColor(productColor.ProductId, productColor.ColorId))
            {
               return false;
            }
            await  _dbContext.ProductColors.AddAsync(productColor);
            return true;
        }

        public void UpdProductColor(ProductColor productColor)
        {
               // _dbContext.ProductColors.Update(productColor);           
        }

        public async Task DeleteProductColor(ProductColor productcolor)
        {
            var productid = productcolor.ProductId;
            var colorid = productcolor.ColorId;              
            productcolor.Deleted = DateTime.UtcNow;
            await ProductImageUpdDeletedRange(productid, colorid);
            await SkuUpdDeletedRange(productid, colorid);


            //_dbContext.ProductColors.Remove(productcolor);
            //_dbContext.ProductImages.RemoveRange(existProductImages);

        }

        private async Task ProductImageUpdDeletedRange(int productid, int colorid)
        {
            var existProductImages = _dbContext.ProductImages.Where(pi =>
                       pi.Deleted == null
                       && pi.ProductId == productid
                       && pi.ColorId == colorid);
            await existProductImages.ForEachAsync(i => i.Deleted = DateTime.UtcNow);
            _dbContext.ProductImages.UpdateRange(existProductImages);
        }

        private async Task SkuUpdDeletedRange(int productid, int colorid)
        {
            var existsSKUs = _dbContext.Skus.Where(s => s.Deleted == null
                       && s.ProductId == productid
                       && s.ColorId == colorid);
            await existsSKUs.ForEachAsync(s => s.Deleted = DateTime.UtcNow);
            _dbContext.Skus.UpdateRange(existsSKUs);
        }

        //public bool ProductColorExists(int productid, int colorid, int? productcolorid)
        //{
        //    if(productcolorid == null)
        //        return (_dbContext.ProductColors.Any(pc => pc.ProductId == productid
        //                                   && pc.ColorId == colorid
        //                                   && pc.Deleted == null));

        //    return (_dbContext.ProductColors.Any(pc => pc.Id == productcolorid
        //                                   && pc.ProductId == productid
        //                                  && pc.ColorId == colorid
        //                                  && pc.Deleted == null));
        //}

        public string GetProductcolorDefaultImageUrl(int productid, int colorid)
        {            
            return _dbContext.ProductImages.Where(pi => pi.ProductId == productid
                                                && pi.ColorId == colorid
                                                && pi.Deleted == null
                                                && pi.ImageTypeCode == "FRN").FirstOrDefault().ProductImageUrl; ;
        }

     
        public async Task<IEnumerable<ProductColor>> GetProductColorsByIdAsync(int productid, int? colorid)
        {
            return await (from pc in _dbContext.ProductColors
                                       join c in _dbContext.Colors on pc.ColorId equals c.Id
                                       join p in _dbContext.Products on pc.ProductId equals p.Id
                                       where pc.ProductId == productid
                                           && pc.Deleted == null
                                           && (colorid == null || pc.ColorId == colorid)
                                       select new ProductColor
                                       {
                                           Id = pc.Id,
                                           ProductId = pc.ProductId,
                                           ColorId = pc.ColorId,
                                           ColorDescription = pc.ColorDescription == null ? c.Description : pc.ColorDescription,
                                           ColorPriceOverride = pc.ColorPriceOverride != null ? pc.ColorPriceOverride : p.Price,
                                           IsDefaultColor = pc.IsDefaultColor
                                       }
                                ).ToListAsync();

          
        }

        public async Task<ProductColor> GetProductColorByProductAndColorIdAsync(int productid, int colorid)
        {

            return await _dbContext.ProductColors.Where(pc => pc.Deleted == null
                         && pc.ProductId == productid
                         && pc.ColorId == colorid).FirstOrDefaultAsync();
        }
    }
}
