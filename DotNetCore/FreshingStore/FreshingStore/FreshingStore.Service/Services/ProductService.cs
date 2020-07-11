using FreshingStore.Core.Entities;
using FreshingStore.Repo.DataAccess;
using FreshingStore.Service.Interface;
using FreshingStore.Service.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreshingStore.Service.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(AppDBContext appDBContext) : base(appDBContext) { }

        public async Task<IEnumerable<Product>> FindProductAsyn(Expression<Func<Product, bool>> predicate)
        {
            return await _dbContext.Products.Where(predicate).ToListAsync(); ;
            // return await _repositoryWrapper.ProductRepo.FindAsync(predicate);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.Where(p => p.Deleted == null).ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<int> ids)
        {
            return await _dbContext.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductResourceParameters productResourcParameters)
        {
            if (productResourcParameters == null)
                return await GetProductsAsync();

            if (string.IsNullOrWhiteSpace(productResourcParameters.ProductName)
                    && string.IsNullOrWhiteSpace(productResourcParameters.SearchQuery))
                return await GetProductsAsync();

            var collection = _dbContext.Products as IQueryable<Product>;
            if (!string.IsNullOrWhiteSpace(productResourcParameters.ProductName))
            {
                var productName = productResourcParameters.ProductName.Trim();
                collection = collection.Where(p => p.Deleted == null && p.ProductName.Contains(productName));
            }
            if (!string.IsNullOrWhiteSpace(productResourcParameters.SearchQuery))
            {
                var searchQuery = productResourcParameters.SearchQuery.Trim();
                collection = collection.Where(p => p.ProductName.Contains(searchQuery)
                                                || p.ProductDescription.Contains(searchQuery));
            }

            return await collection.ToListAsync();

        }

        public void RemoveProduct(Product product)
        {
            _dbContext.Products.Remove(product);
        }

        public void UpdProduct(Product product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            await _dbContext.Products.AddAsync(product);
          

        }

        public bool ProductExist(int productid)
        {
           return _dbContext.Products.Any(p => p.Id == productid);

        }

    }
    
}
