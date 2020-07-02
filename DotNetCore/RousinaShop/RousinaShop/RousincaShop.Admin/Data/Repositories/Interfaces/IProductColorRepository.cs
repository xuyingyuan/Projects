

using RousincaShop.Admin.Data.Entities;
using System.Collections.Generic;

namespace RousincaShop.Admin.Data.Repositories.Interfaces
{
    public interface IProductColorRepository : IGenericRepository<Entities.ProductColor>
    {
        IEnumerable<ProductColor> GetProductcolorbyProductId(int productId);
    }
}
