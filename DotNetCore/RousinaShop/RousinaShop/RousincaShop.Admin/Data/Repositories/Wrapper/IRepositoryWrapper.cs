using RousincaShop.Admin.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Data.RepostoryWrap
{
    public interface IRepositoryWrapper
    {
        IProductColorRepository ProductColor { get; }
        IProductImageRepository ProductImage { get; }
        IProductRepository Product { get; }

        void Save();
    }
}
