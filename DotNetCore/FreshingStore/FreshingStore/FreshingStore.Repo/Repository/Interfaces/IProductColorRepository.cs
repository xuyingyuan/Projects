using FreshingStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository.Interfaces
{
    public interface IProductColorRepository:IRepository<ProductColor>
    {
        Task<IEnumerable<ProductColor>> GetProductColorsByIdAsync(int productid, int? colorid);
        string GetProductColorDefaultImageUrl(int productid, int colorid);
    }
}
