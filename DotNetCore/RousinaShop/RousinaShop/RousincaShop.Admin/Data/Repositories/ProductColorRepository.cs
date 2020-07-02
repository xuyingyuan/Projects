using RousincaShop.Admin.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace RousincaShop.Admin.Data.Repositories.Interfaces
{
    public class ProductColorRepository : GenericRepository<Entities.ProductColor>, IProductColorRepository
    {
       
        public ProductColorRepository(RousinaDBContext dbcontext) : base(dbcontext) { }

        public IEnumerable<ProductColor> GetProductcolorbyProductId(int productId)
        {
            return _dbContext.ProductColors.Where(pc => pc.ProductId == productId);

        }
    }
}
