using Microsoft.CodeAnalysis.CSharp.Syntax;
using NLog;
using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IloggerService _logger;
        public ProductService(IProductRepository productRepository, IloggerService iloggerService)
        {
            _productRepository = productRepository;
            _logger = iloggerService;
        }
        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return _productRepository.Find(predicate);
        }

        public Product Get(int id)
        {
            if (id <= 1)
            {
                //throw new InvalidProgramException("id should bigger than one");
                _logger.LogInformation("Error: id should bigger than one");
                return null;
            }
            var product = _productRepository.Get(id);
            if (product == null)
            {
                _logger.LogInformation("Unable to Retrieved a product with Id: {Id}", id);
                return null;
            }
            _logger.LogInformation("Retrieved a product with Id: {Id}", id);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _productRepository.GetAsync(id);
        }

        public async Task<Product> GetDetailAsync(int id, string[] subset)
        {
            return await _productRepository.GetDetailAsync(id, subset);
        }
    }
}
