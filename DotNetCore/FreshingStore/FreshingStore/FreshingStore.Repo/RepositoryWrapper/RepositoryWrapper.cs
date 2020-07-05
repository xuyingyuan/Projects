using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository;
using FreshingStore.Repo.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreshingStore.Repo.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDBContext _dbContext;
        private IProductColorRepository _productcolorRepo;
        private IProductImageRepository _productImageRepo;
        private IProductRepository _product;

        public RepositoryWrapper(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductColorRepository ProductColorRepo
        {
            get
            {
                if (_productcolorRepo == null)
                {
                    _productcolorRepo = new ProductColorRepository(_dbContext);
                }
                return _productcolorRepo;
            }
        }

        public IProductImageRepository ProductImageRepo
        {
            get
            {
                if (_productImageRepo == null)
                {
                    _productImageRepo = new ProductImageRepository(_dbContext);
                }
                return _productImageRepo;
            }
        }

        public IProductRepository ProductRepo
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_dbContext);
                }
                return _product;
            }
        }


        

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
