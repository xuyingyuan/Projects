using FreshingStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Interface
{
   public  interface IProductColorService:IbaseService
    {
        Task<IEnumerable<ProductColor>> GetProductColorsByIdAsync(int productid);
        Task<IEnumerable<ProductColor>> GetProductColorsByIdAsync(int productid, int colorid);
        Task<IEnumerable<ProductColor>> GetProductColorsByIdAsync(int productid, IEnumerable<int> colorids);

        Task<ProductColor> GetProductColorByProductAndColorIdAsync(int productid, int colorid);

        Task AddProductColorAsync(ProductColor productColor);
        
        void UpdProductColor(ProductColor productColor);
        void RemoveProductColor(ProductColor productcolor);

        string GetProductcolorDefaultImageUrl(int productid, int colorid);

  

       
       
    }
}
