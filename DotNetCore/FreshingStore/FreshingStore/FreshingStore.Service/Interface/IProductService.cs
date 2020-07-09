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
       
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate);
        Task  AddProductAsync(Product product);
        void UpdProduct(Product product);
        void RemoveProduct(Product product);

        void Commit();
        Task CommitAsync();

    }
}
