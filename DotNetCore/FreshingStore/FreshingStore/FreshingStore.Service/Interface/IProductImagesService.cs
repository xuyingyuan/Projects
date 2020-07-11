using FreshingStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Interface
{
    public interface IProductImagesService:IbaseService
    {
        Task<IEnumerable<ProductImage>> GetProductImages(int productid, int? colorid, string defaultType = "");
    }
}
