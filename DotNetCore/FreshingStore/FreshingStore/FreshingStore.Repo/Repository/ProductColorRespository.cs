using FreshingStore.Core.Entities;
using FreshingStore.Repo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository
{
    public class ProductColorRespository : Repository<ProductColor>, IProductColorRepository
    {
        public ProductColorRespository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public string GetProductColorDefaultImageUrl(int productid, int colorid)
        {
            return _dbContext.ProductImages.Where(pi => pi.ProductId == productid 
                                                && pi.ColorId == colorid
                                                && pi.Deleted == null
                                                && pi.ImageTypeCode=="FRN").FirstOrDefault().ProductImageUrl;
        }

        public async Task<IEnumerable<ProductColor>> GetProductColorsByProductIdAsync(int productid)
        {
            var productcolors = await (from pc in _dbContext.ProductColors
                                 join c in _dbContext.Colors  on pc.ColorId equals c.Id  
                                 join p in _dbContext.Products on pc.ProductId equals p.Id                              
                                where pc.ProductId == productid && pc.Deleted == null
                                select new ProductColor
                                {
                                    Id = pc.Id,
                                    ProductId=pc.ProductId,
                                    ColorId=pc.ColorId,
                                    ColorDescription=pc.ColorDescription==null?c.Description: pc.ColorDescription,
                                    ColorPriceOverride=pc.ColorPriceOverride!=null ? pc.ColorPriceOverride: p.Price,
                                    IsDefaultColor = pc.IsDefaultColor
                                }
                                ).ToListAsync();            
                      
            return productcolors;

        }

    }
}
