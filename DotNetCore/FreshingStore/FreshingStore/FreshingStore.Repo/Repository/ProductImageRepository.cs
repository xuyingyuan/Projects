using FreshingStore.Core.Entities;
using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository.Interfaces;

namespace FreshingStore.Repo.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(AppDBContext dbcontext) : base(dbcontext) { }
    }
}
