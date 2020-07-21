using RousincaShop.Admin.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Service
{
    interface IProductService
    {
        Task<Product> GetDetailAsync(int id, string[] subset);

        Product Get(int id);
        Task<Product> GetAsync(int id);
        IEnumerable<Product> GetAll();
        Task<IEnumerable<Product>> GetAllAsync();
        IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate);
        //  Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
