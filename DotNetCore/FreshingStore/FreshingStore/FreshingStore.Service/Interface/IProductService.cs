using FreshingStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Interface
{
    public interface IProductService
    {
        Product GetProduct(int id);
        Task<IEnumerable<Product>> GetProductsAsyn();
        Task<Product> GetProductAsyn(int id);
        Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate);
        Task  AddProductAsyn(Product product);
        void RemoveProduct(Product product);

    }
}
