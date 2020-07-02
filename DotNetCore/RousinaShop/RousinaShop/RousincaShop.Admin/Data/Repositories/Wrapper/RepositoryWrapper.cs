using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Data.RepostoryWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Data.Repositories.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RousinaDBContext _rousinaDBContext;
        private IProductColorRepository _productcolor;
        private IProductImageRepository _productImage;
        private IProductRepository _product;

        public RepositoryWrapper(RousinaDBContext rousinaDBContext)
        {
            _rousinaDBContext = rousinaDBContext;
        }
        public IProductColorRepository ProductColor 
        {
            get 
            { 
                if (_productcolor == null) 
                {
                    _productcolor = new ProductColorRepository(_rousinaDBContext); 
                }
                return _productcolor;
            }
        }

        public IProductImageRepository ProductImage
        {
            get
            {
                if (_productImage == null)
                {
                    _productImage = new ProductImageRepository(_rousinaDBContext);
                }
                return _productImage;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_rousinaDBContext);
                }
                return _product;
            }
        }

        public void Save()
        {
            _rousinaDBContext.SaveChanges();
        }
    }
}
