using FreshingStore.Core.Entities;
using FreshingStore.Repo.Repository;
using FreshingStore.Repo.Repository.Interfaces;
using FreshingStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Services
{
    public class ProductColorService: IProductColorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductColorRepository _productcolor;
       


        public ProductColorService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _productcolor = new ProductColorRespository(_uow);
           
        }

        public string GetProductcolorDefaultImageUrl(int productid, int colorid)
        {
            return _productcolor.GetProductColorDefaultImageUrl(productid, colorid);
        }

        public async Task<IEnumerable<ProductColor>>  GetProductColorsByProductIdAsync(int productid)
        {
            var Productcolors = await _productcolor.GetProductColorsByProductIdAsync(productid);
            return Productcolors;
        }
    }
}
