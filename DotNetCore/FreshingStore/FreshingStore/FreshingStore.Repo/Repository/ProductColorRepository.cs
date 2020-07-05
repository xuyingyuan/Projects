using FreshingStore.Core.Entities;
using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository.Interfaces;

namespace FreshingStore.Repo.Repository
{
    public class ProductColorRepository : Repository<ProductColor>, IProductColorRepository
    {
        public ProductColorRepository(AppDBContext dbcontext) : base(dbcontext) { }
    }
}
