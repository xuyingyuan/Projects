using Microsoft.EntityFrameworkCore;
using RousincaShop.Admin.Data.Entities;

using RousincaShop.Admin.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(RousinaDBContext dbcontext) : base(dbcontext) { }

        public async Task<Product> GetDetailAsync(int id, string[] subset)
        {
            var product = await _dbContext.Products.SingleAsync(p => p.Id == id);

            foreach(var sub in subset)
            {
                switch (sub)
                {
                    case "productimages":
                        _dbContext.Entry(product)
                             .Collection(p => p.ProductImages)
                             .Query().Include(i=>i.ImageTypeNavigation)
                             .Load();
                        break;
                    case "skus":
                        _dbContext.Entry(product)
                             .Collection(p => p.Skus)
                             .Query().Include(i => i.SizeCodeNavigation)
                             .Load();
                        break;
                    default:break;
                }
                  
            }

            return product;

        }

      
    }
}
