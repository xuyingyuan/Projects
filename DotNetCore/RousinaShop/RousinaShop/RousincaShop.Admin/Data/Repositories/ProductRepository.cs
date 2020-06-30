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
    }
}
