using FreshingStore.Core.Entities;
using FreshingStore.Repo.DataAccess;
using FreshingStore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Services
{
    public class ProductImagesService: BaseService, IProductImagesService
    {
        

        public ProductImagesService(AppDBContext appDBContext):base(appDBContext)
        {
           
        }

        public async Task<IEnumerable<ProductImage>> GetProductImages(int productid, int? colorid, string defaultType="")
        {

            return await _dbContext.ProductImages.Where(pi => pi.ProductId == productid
                                   && (colorid == null || pi.ColorId == colorid.Value)
                                   && (defaultType == "" || pi.ImageTypeCode == defaultType)).ToListAsync();
        }

    }
}
