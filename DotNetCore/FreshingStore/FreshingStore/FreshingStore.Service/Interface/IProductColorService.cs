using FreshingStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Interface
{
   public  interface IProductColorService
    {
        Task<IEnumerable<ProductColor>> GetProductColorsByIdAsync(int productid, int? colorid);
        string GetProductcolorDefaultImageUrl(int productid, int colorid);

       
    }
}
