using RousincaShop.Admin.Data.Entities;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Data.Repositories.Interfaces
{
    public  interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetDetailAsync(int id, string[] subset);

      
    
    }
}
