using FreshingStore.Core.Entities;
using FreshingStore.Repo.Repository;
using FreshingStore.Repo.Repository.Interfaces;
using FreshingStore.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreshingStore.Service.Services
{
    public class ProductService : IProductService
    {
       
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Product> _repo;
      

        public ProductService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _repo = new Repository<Product>(_uow);
          
           
        }     
        
        public async Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate)
        {
                return await _repo.FindAsync(predicate);
            // return await _repositoryWrapper.ProductRepo.FindAsync(predicate);
        }

        public async Task<Product> GetProductAsync(int id)
        {         
            return await _repo.GetAsync(id);
        }

        public Product GetProduct(int id)
        {
           
            return _repo.Get(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            
            return await _repo.GetAllAsync();
        }

        public  void RemoveProduct(Product product)
        {
            _repo.Remove(product); 
        }

        public void UpdProduct(Product product)
        {
            _repo.Update(product);
        }

        public async Task AddProductAsync(Product product)
        {
            await _repo.AddAsync(product);         
        }

        //public async Task<IEnumerable<ProductColor>> GetProductColorAsync(int ProductId)
        //{
        //    var productcolors = await _colorRepo.FindAsync(e=>e.ProductId== ProductId);
        //    return productcolors;
        //}


        public void Commit()
        {
            _uow.Commit();
        }
        public async Task CommitAsync()
        {
          await  _uow.CommitAsync();
        }

      
    }
}
