using RousincaShop.Admin.Data.Entities;

namespace RousincaShop.Admin.Data.Repositories.Interfaces
{
    public class SizeCodeRepository : GenericRepository<SizeCode>, ISizeCodeRepository
    {
        public SizeCodeRepository(RousinaDBContext dbcontext) : base(dbcontext) { }
    }
}
