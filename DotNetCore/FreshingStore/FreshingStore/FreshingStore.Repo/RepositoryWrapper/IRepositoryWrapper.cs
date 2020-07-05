using FreshingStore.Repo.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreshingStore.Repo.RepositoryWrapper
{
   public  interface IRepositoryWrapper
    {
        IProductColorRepository ProductColorRepo { get; }
        IProductImageRepository ProductImageRepo { get; }
        IProductRepository ProductRepo { get; }

        void Save();
    }
}
