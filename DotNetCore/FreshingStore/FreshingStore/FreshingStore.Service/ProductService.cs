using FreshingStore.Core.Entities;
using FreshingStore.Repo.Repository.Interfaces;
using FreshingStore.Repo.RepositoryWrapper;
using FreshingStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace FreshingStore.Service
{
    public class ProductService : IProductService
    {
        //private IRepositoryWrapper _repositoryWrapper;
        private readonly IUnitOfWork _uow;
        private readonly IRepository1<Product> _repo;
        public ProductService(IUnitOfWork unitOfWork, IRepository1<Product> repo)
        {
            _uow = unitOfWork;
            _repo = repo;
        }     
        
        public async Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate)
        {
                return await _repo.FindAsync(predicate);
            // return await _repositoryWrapper.ProductRepo.FindAsync(predicate);
        }

        public async Task<Product> GetProductAsyn(int id)
        {
            return await _repo.GetAsync(id);
        }

        public Product GetProduct(int id)
        {
            return _repo.Get(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsyn()
        {
           return await _repo.GetAllAsync();
        }

        public  void RemoveProduct(Product product)
        {
            _repo.Remove(product); 
        }

        public async Task AddProductAsyn(Product product)
        {
            await _repo.AddAync(product);         
        }

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
