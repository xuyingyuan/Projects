using FreshingStore.Core.Entities;
using FreshingStore.Repo.RepositoryWrapper;
using FreshingStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreshingStore.Service
{
    public class ProductService : IProductService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public ProductService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
       
        

        public async Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate)
        {
            return await _repositoryWrapper.ProductRepo.FindAsync(predicate);
        }

        public async Task<Product> GetProductAsyn(int id)
        {
            return await _repositoryWrapper.ProductRepo.GetAsync(id);
        }

        public Product GetProduct(int id)
        {
            return  _repositoryWrapper.ProductRepo.Get(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsyn()
        {
           return await _repositoryWrapper.ProductRepo.GetAllAsync();
        }

        public  void RemoveProduct(Product product)
        {
            _repositoryWrapper.ProductRepo.Remove(product);
            _repositoryWrapper.Save();

        }

        public async Task AddProductAsyn(Product product)
        {
            await _repositoryWrapper.ProductRepo.AddAync(product);
            _repositoryWrapper.Save();
        }

     
    }
}
