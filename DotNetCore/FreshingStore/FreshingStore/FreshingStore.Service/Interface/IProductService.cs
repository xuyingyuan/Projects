using FreshingStore.Core.Entities;
using FreshingStore.Service.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Interface
{
    public interface IProductService:IbaseService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Product>> GetProductsAsync(ProductResourceParameters productResourcParameters);
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate);
        Task  AddProductAsync(Product product);
       
        void UpdProduct(Product product);
        void RemoveProduct(Product product);

     

    }
}
